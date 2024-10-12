<template>
  <div>
    <span>注册</span>
    <a
      :href="`http://124.222.216.19:8081/#/?backUrl=${root}/#${route.fullPath}`"
      >登录</a
    >
  </div>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router';

const route = useRoute();
const root = location.origin;
</script>

<style lang="scss" scoped>
span,
a {
  margin-left: 20px;
  color: #000;
  text-decoration: none;
  &:active {
    color: #000;
  }
}
</style>
