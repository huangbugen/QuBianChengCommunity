import { superAxios } from './superAxios';

export const getLoginPageSetting = async () => {
  var res = await (
    await superAxios.useLocal()
  ).get({
    url: 'appsettings.json' + '?t=' + new Date().getTime().toString(),
  });
  return res.data.LoginPageInfo;
};

export const checkLogin = async (params: any) => {
  var res = await (
    await superAxios.useWebApi('UserService')
  ).get({
    url: 'api/User/CheckLogin',
    params,
  });
  return res.data;
};
