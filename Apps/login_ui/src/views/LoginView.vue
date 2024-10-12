<template>
  <div id="userLogin" v-if="settings != null">
    <div
      class="middle-pad"
      :style="{ backgroundImage: `url('${settings.backgroundImage}')` }">
      <h1 :style="{ backgroundImage: `url('${settings.logo}')` }">
        {{ settings.pageName }}
      </h1>
      <div class="login-pad">
        <h3 class="login-title">{{ settings.loginPadName }}</h3>
        <div class="login-options">
          <p>
            <el-input
              v-model="userNo"
              placeholder="请输入手机号码 或 Email"
              size="large">
              <template #prepend>
                <el-icon>
                  <Lock />
                </el-icon>
              </template>
            </el-input>
          </p>
          <p>
            <el-input
              size="large"
              type="password"
              show-password
              v-model="password"
              placeholder="请输入密码">
              <template #prepend>
                <el-icon>
                  <Lock />
                </el-icon>
              </template>
            </el-input>
          </p>
          <p>
            <el-button type="primary" size="large" @click="onLogin"
              >登录</el-button
            >
          </p>
          <p class="options">
            <router-link to="/register" class="register-link"
              >注册账号</router-link
            >
            <span>&emsp;</span>
            <router-link to="/changePassword" class="register-link"
              >忘记密码</router-link
            >
          </p>
        </div>
      </div>
    </div>
    <div class="bottom-pad">
      <p>{{ settings.copyright }}</p>
      <p v-for="content in settings.copyrightInfos">{{ content }}</p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { cookieHelper } from '@/common/cookieHelper';
import { checkLogin, getLoginPageSetting } from '@/httpRequest/loginRequest';
import { onMounted, ref } from 'vue';
import { Lock } from '@element-plus/icons-vue';
import { useRouter } from 'vue-router';

const settings = ref();
const userNo = ref('');
const password = ref('');

const router = useRouter();

const onLogin = async () => {
  var res = await checkLogin({
    userNo: userNo.value,
    password: password.value,
  });

  if (res != null) {
    cookieHelper.setCookie('token', res);

    // console.log(location.href);

    location.href = location.href.split('?backUrl=')[1];
  }
};

onMounted(async () => {
  settings.value = await getLoginPageSetting();
});
</script>

<style lang="scss" scoped>
#userLogin {
  height: 100vh;
  position: relative;
  min-height: 700px;
  overflow: hidden;

  .middle-pad {
    background-image: url('../../public/images/login-background2.jpg');
    background-size: 100%;
    height: 70%;
    min-height: 300px;
    background-repeat: no-repeat;
    margin-top: 120px;
    position: relative;

    > h1 {
      position: absolute;
      top: -90px;
      background-size: 76px 76px;
      height: 76px;
      background-repeat: no-repeat;
      padding-left: 80px;
      line-height: 76px;
      font-family: '华文中宋';
      color: rgb(0, 56, 166);
      font-weight: 800;
      font-size: 56px;
      text-shadow: 3px 3px 3px rgba(0, 0, 0, 0.36);
      margin-left: 20px;
    }

    .login-pad {
      background: linear-gradient(#fff, #eee);
      width: 356px;
      height: 316px;
      position: absolute;
      left: 0;
      right: -50%;
      top: 0;
      bottom: 0;
      margin: auto;
      padding: 16px;
      border-radius: 6px;
      border: 1px solid #eee;
      box-shadow: 0 0 6px rgba(0, 0, 0, 0.58);

      .login-title {
        color: rgb(26, 124, 223);
        text-align: center;
        font-size: 22px;
        font-weight: 800;
        padding-bottom: 10px;
        margin-bottom: 36px;
        border-bottom: 1px solid #ccc;
      }

      .login-options {
        text-align: left;
        width: 80%;
        margin: auto;

        p {
          margin-top: 16px;

          .el-button {
            width: 100%;
          }
        }

        .options {
          text-align: center;
          font-size: 13px;

          :deep(a) {
            color: #666;
            font-style: normal;
            text-decoration: none;

            &:active {
              color: #666;
            }
          }
        }
      }
    }
  }

  .bottom-pad {
    height: 100px;
    position: absolute;
    bottom: 0;
    width: 100%;

    p {
      text-align: center;
      margin-top: 10px;
    }
  }
}
</style>
