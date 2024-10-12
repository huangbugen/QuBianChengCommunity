<template>
  <div>
    <Suspense>
      <template #default>
        <router-view></router-view>
      </template>
      <template #fallback> 加载中... </template>
    </Suspense>
  </div>
</template>

<script setup lang="ts"></script>

<style scoped></style>
