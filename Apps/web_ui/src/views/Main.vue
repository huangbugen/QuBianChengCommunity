<template>
  <div>
    <div class="nav-pad">
      <el-menu mode="horizontal" :default-active="activeIndex">
        <el-menu-item index="First" @click="toPage('/')">首页</el-menu-item>
        <el-menu-item index="FileType" @click="toPage('/file-type')"
          >文件下载</el-menu-item
        >
        <el-menu-item index="3" @click="toPage('/')" disabled
          >图书仓库</el-menu-item
        >
        <el-menu-item index="Section" @click="toPage('/section')"
          >社区论坛</el-menu-item
        >
      </el-menu>
      <TopLoginPad class="top-login-pad" />
    </div>
    <div class="page-pad">
      <router-view :key="route.fullPath"></router-view>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import TopLoginPad from '../components/TopLoginPad.vue';
import { getCurrentComponentName } from '@/utility/RouterHelper';
import { connection } from '@/utility/signalr';

connection.on('onConnectionAsync', (res) => {
  console.log(res);
});

const route = useRoute();

const activeIndex = ref('First');
activeIndex.value = getCurrentComponentName() as string;

const router = useRouter();
const toPage = (path: string) => {
  router.push(path);
};
</script>

<style lang="scss" scoped>
.nav-pad {
  width: 80%;
  min-width: 1366px;
  margin: auto;
  position: relative;

  .top-login-pad {
    position: absolute;
    bottom: 10px;
    right: 10px;
  }
}

.page-pad {
  width: 80%;
  min-width: 1366px;
  margin: auto;
}
</style>
