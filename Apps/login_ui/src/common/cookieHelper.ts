class CookieHelper {
    /**
     * 设置cookie
     * @param key cookie的🗡
     * @param value cookie的值
     * @param keepTime 保存登录状态的时间
     */
    setCookie = (key: string, value: any, keepTime: any = null) => {
        // 登陆时间
        var loginTime = new Date();
        if (keepTime != null) {
            // 设置保存时间
            loginTime.setTime(loginTime.getTime() + 24 * 60 * 60 * 1000 * keepTime);
            // 设置过期时间
            var expires = "expires=" + loginTime.toString();
            // 拼接cookie字符串
            window.document.cookie = `${key}=${value};${expires}`;
        } else {
            window.document.cookie = `${key}=${value}`;
        }
    };

    /**
     * 获取cookie
     * @param key cookie的🗡
     * @returns 
     */
    getCookie = (key: string) => {
        var cookieValue = null;
        if (document.cookie.length > 0) {
            // 获取cookie数组
            let cArr = document.cookie.split(";");
            for (let i = 0; i < cArr.length; i++) {
                const newArr = cArr[i].split("=");
                if (newArr[0].trim() === key) {
                    cookieValue = newArr[1].trim();
                    break;
                }
            }
        }
        return cookieValue;
    }
}

// export const cookieHelper, 将 cookieHelper 变量导出，使其在其他模块中可用。const 关键字表明 cookieHelper 是一个常量，一旦赋值后其引用不可更改。
// (function () { ... })(); 是一个立即执行函数表达式。这种函数在定义后立即执行，并且只执行一次。
export const cookieHelper = (function () {
    return new CookieHelper();
})();