import { superAxios } from './superAxios';

export const registUser = async (params: any) => {
  var res = await (
    await superAxios.useWebApi('UserService')
  ).post({
    url: 'api/User',
    params,
  });
  return res.data;
};
