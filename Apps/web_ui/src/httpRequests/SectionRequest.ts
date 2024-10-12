import { superAxios } from "./super-axios"

export const getAreaInfo = async () => {
    const res = await (await superAxios().useWebApi()).get({
        url: "/bbs/Area?pageIndex=1&pageSize=30"
    })

    return res.data;
}