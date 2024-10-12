import { superAxios } from './super-axios';

export const getPosts = async (pid: string) => {
  const res = await (
    await superAxios().useWebApi()
  ).get({
    url: `/bbs/Post/${pid}/1`,
  });

  return res.data;
};

export const getReplies = async (pid: string, pageIndex: number) => {
  const res = await (
    await superAxios().useWebApi()
  ).get({
    url: `/bbs/reply/${pid}/${pageIndex}`,
  });

  return res.data;
};

export const addReplies = async (params: any) => {
  const res = await (
    await superAxios().useWebApi()
  ).post({
    url: `/bbs/reply`,
    params,
  });

  return res.data;
};
