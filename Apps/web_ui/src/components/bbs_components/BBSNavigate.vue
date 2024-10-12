<template>
  <nav>
    <el-breadcrumb :separator-icon="ArrowRight">
      <el-breadcrumb-item :to="{ path: '/' }">
        <el-icon style="color: #c5d7e3">
          <HomeFilled />
        </el-icon>
      </el-breadcrumb-item>
      <el-breadcrumb-item :to="{ path: '/' }">
        <span style="font-weight: normal">首页</span>
      </el-breadcrumb-item>
      <el-breadcrumb-item v-for="navigate in navigates" :to="navigate.url">
        <span style="font-weight: normal">{{ navigate.name }}</span>
      </el-breadcrumb-item>
    </el-breadcrumb>
  </nav>
</template>

<script setup lang="ts">
import { ArrowRight, HomeFilled } from '@element-plus/icons-vue';
const props = defineProps({
  navigates: Object,
});
const navigates = props.navigates;
</script>

<style scoped lang="scss">
nav {
  padding: 10px 0;
}
</style>
