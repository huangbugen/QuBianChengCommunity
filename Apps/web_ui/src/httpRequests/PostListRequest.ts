import { superAxios } from "./super-axios";

export const getSectionInfo = async (sid: string) => {
    const res = await (await superAxios().useWebApi()).get({
        url: `/bbs/Section?sectionId=${sid}`,
    })

    return res.data;
}

export const getPosts = async (params:any) => {
    const res = await (await superAxios().useWebApi()).get({
        url: `/bbs/Post`,
        params
    })

    return res.data;
}

export const getPostType = async (sid: string) => {
    const res = await (await superAxios().useWebApi()).get({
        url: `/bbs/Post/PostType?sectionId=${sid}`,
    })

    return res.data;
}

export const AddPost = async (params: any) => {
    const res = await (await superAxios().useWebApi()).post({
        url: `/bbs/Post`,
        params
    })

    return res.data;
}