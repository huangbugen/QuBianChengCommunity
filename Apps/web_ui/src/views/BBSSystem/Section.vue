<template>
  <div>
    <BBSNavigate :navigates="navigates"></BBSNavigate>
    <AreaSectionPad :areaInfo="areaInfo"></AreaSectionPad>
  </div>
</template>

<script setup lang="ts">
import AreaSectionPad from '@/components/bbs_components/AreaSectionPad.vue';
import BBSNavigate from '@/components/bbs_components/BBSNavigate.vue';
import { getAreaInfo } from '@/httpRequests/SectionRequest';
import { onMounted } from 'vue';
import { ref } from 'vue';

const areaInfo = ref();
const navigates = ref([{ name: '社区论坛', url: '' }]);

onMounted(async () => {
  var res = await getAreaInfo();
  areaInfo.value = res;
});
</script>

<style scoped lang="scss"></style>
