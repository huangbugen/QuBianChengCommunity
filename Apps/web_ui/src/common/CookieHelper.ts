class CookieHelper {
    /**
     * 设置cookie
     * @param key cookie的🗡
     * @param value cookie的值
     * @param keepTime 保存登录状态的时间
     */
    setCookie = (key: string, value: any, keepTime: any = null) => {
        // 登录时间
        var loginTime = new Date();
        if (keepTime != null) {
            // 设置保存时间
            keepTime = keepTime == null ? "999" : keepTime;
            loginTime.setTime(loginTime.getTime() + 24 * 60 * 60 * 1000 * keepTime)
            // 设置过期时间
            var expires = 'expires=' + loginTime.toString();
            // 拼接cookie字符串
            window.document.cookie = `${key}=${value};${expires};domain=qubcedu.net;path=/`
        }
        else {
            window.document.cookie = `${key}=${value};domain=qubcedu.net;path=/`
        }
    }
    /**
     * 获取cookie
     * @param key cookie的🗡
     */
    getCookie = (key: string) => {
        var cookieValue = null;
        if (document.cookie.length > 0) {
            // 获取cookie数组
            let cArr = document.cookie.split(';');
            for (let i = 0; i < cArr.length; i++) {
                const newArr = cArr[i].split('=');
                if(newArr[0].trim() === key)
                {
                    cookieValue = newArr[1].trim();
                    break;
                }
            }
        }
        return cookieValue;
    }
}
export const cookieHelper = (function () {
    return new CookieHelper();
})();