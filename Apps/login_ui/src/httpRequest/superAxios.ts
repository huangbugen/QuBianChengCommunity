import { cookieHelper } from "@/common/cookieHelper";
import axios from "axios";

class AxiosHelper {
    private baseUrl: string;

    constructor() {
        this.baseUrl = "/";
        axios.defaults.withCredentials = true;

        // 限制访问次数，防止无线循环访问

        let i = 0;
        // 拦截器。在请求或响应被 then 或 catch 处理之前，先对它们进行拦截处理。
        // 通过使用拦截器，你可以统一处理请求或响应，比如添加认证头、处理错误、统一日志记录等。
        // use支持两个参数第一个成功时调用，第二个失败是调用
        axios.interceptors.response.use(async (res: any) => {
            if (res.status == 203) {
                if (i++ <= 0) {
                    // 重新请求RefreshToken接口，去刷新token
                    var refreshToken = await (
                        await this.useWebApi()
                    ).get({
                        url: "api/User/RefreshToken",
                    });

                    // 如果结果不为空则将新的token返回
                    if (refreshToken != null) {
                        // 将返回的token写入cookie
                        cookieHelper.setCookie("token", refreshToken.data);
                        // 清空baseURL因为res.request.responseURL记录着完整的请求地址
                        this.baseUrl = "";
                        // 从cookie中获取token并加入到请求头中
                        await this.getCookie();
                        // 请求之前请求失败的地址
                        await this.get({
                            url: res.request.responseURL,
                        });
                    }
                }
            }
            return res;
        });
    }

    async useWebApi(apiRootKey: string = "Default") {
        await this.getCookie();
        this.baseUrl = await this.getValue(apiRootKey);
        return this;
    }

    async useLocal() {
        this.baseUrl = await this.getValue("Local");
        return this;
    }

    async post(data: any) {
        var res = await axios.post(`${this.baseUrl}${data.url}`, data.params);
        return res;
    }

    async get(data: any) {
        var res = await axios.get(`${this.baseUrl}${data.url}`, {
            params: data.params,
        });
        return res;
    }

    async put(data: any) {
        var res = await axios.put(`${this.baseUrl}${data.url}`, data.params);
        return res;
    }

    async deleted(data: any) {
        var res = await axios.delete(`${this.baseUrl}${data.url}`, {
            params: data.params,
        });
        return res;
    }

    async getCookie() {
        var token = cookieHelper.getCookie("token");
        axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    }

    async getValue(key: string) {
        var resStr = sessionStorage["apiRootKey"];
        if (resStr == null) {
            let res = await axios.get("/appsettings.json");
            sessionStorage["apiRootKey"] = JSON.stringify(res.data);
            return res.data["ApiRoots"][key];
        } else {
            let res = JSON.parse(resStr);
            return res["ApiRoots"][key];
        }
    }
}

export const superAxios = (function () {
    return new AxiosHelper();
})();

