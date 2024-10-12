<template>
  <div style="width: 50%; margin: 100px auto">
    <el-form
      :model="registData"
      :rules="rules"
      label-width="120px"
      ref="validateForm">
      <el-form-item label=" " prop="userName">
        <el-input v-model="registData.userName" placeholder="请输入用户名">
          <template #prepend>用户名</template>
        </el-input>
      </el-form-item>
      <el-form-item label=" " prop="phone">
        <el-input v-model="registData.phone" placeholder="请输入手机号">
          <template #prepend>手机号</template>
        </el-input>
      </el-form-item>
      <el-form-item label=" " prop="email">
        <el-input v-model="registData.email" placeholder="请输入邮箱">
          <template #prepend>Email</template>
        </el-input>
      </el-form-item>
      <el-form-item label=" " prop="pwd1">
        <el-input v-model="registData.pwd1" placeholder="请输入密码">
          <template #prepend>密码</template>
        </el-input>
      </el-form-item>
      <el-form-item label=" " prop="pwd2">
        <el-input v-model="registData.pwd2" placeholder="请再次输入密码">
          <template #prepend>再次输入密码</template>
        </el-input>
      </el-form-item>
    </el-form>

    <el-button @click="onRegister">注册</el-button>
    <el-button @click="onReset">重置</el-button>
  </div>
</template>

<script setup lang="ts">
import { registUser } from '../httpRequest/registRequest';
import { ref } from 'vue';

const registData = ref({
  userName: '',
  phone: '',
  email: '',
  pwd1: '',
  pwd2: '',
});
const validateEmail = (rule: any, value: any, callback: any) => {
  const reg =
    /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  const res = reg.test(value);
  if (!res) {
    callback(new Error('请输入正确的邮箱'));
  } else {
    callback();
  }
};
const validatePhone = (rule: any, value: any, callback: any) => {
  const reg = /^(?:(?:\+|00)86)?1[3-9]\d{9}$/;
  const res = reg.test(value);
  if (!res) {
    callback(new Error('请输入正确的邮箱'));
  } else {
    callback();
  }
};
const validatePassword = (rule: any, value: any, callback: any) => {
  const res = value == registData.value.pwd1;
  if (!res) {
    callback(new Error('两次密码不一致'));
  } else {
    callback();
  }
};
const rules = ref({
  userName: [{ required: true, message: '请输入用户名', trigger: 'blur' }],
  email: [
    { required: true, message: '请输入邮箱', trigger: 'blur' },
    {
      validator: validateEmail,
      trigger: 'blur',
    },
  ],
  phone: [
    { required: true, message: '请输入手机号', trigger: 'blur' },
    {
      validator: validatePhone,
      trigger: 'blur',
    },
  ],
  pwd1: [{ required: true, message: '请输入密码', trigger: 'blur' }],
  pwd2: [
    { required: true, message: '请输入密码', trigger: 'blur' },
    {
      validator: validatePassword,
      trigger: 'blur',
    },
  ],
});
const validateForm = ref();

const onRegister = () => {
  validateForm.value.validate(async (res: any) => {
    if (res) {
      await registUser({
        userNo: registData.value.phone,
        userName: registData.value.userName,
        password: registData.value.pwd1,
        email: registData.value.email,
      });
      return;
    }
    alert('不给提交');
  });
};

const onReset = () => {
  registData.value.phone = '';
  registData.value.userName = '';
  registData.value.pwd1 = '';
  registData.value.pwd2 = '';
  registData.value.email = '';
};
</script>

<style scoped lang="scss">
p {
  margin-bottom: 20px;
}
</style>
