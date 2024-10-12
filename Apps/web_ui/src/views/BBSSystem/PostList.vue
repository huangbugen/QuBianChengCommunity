<template>
  <div>
    <BBSNavigate :navigates="navigates"></BBSNavigate>
    <div class="bbs-pad">
      <div class="playbill" v-if="section != null">
        <div class="section-info">
          <p>
          <h1>{{ section.sectionTitle }}</h1>
          <span>今日：{{ section.postTodayCount }}</span>
          <span>主题：{{ section.postAllCount }}</span>
          </p>
          <p>
            <span v-for="lorder in section.sectionLorders" style="margin-right: 10px;">
              {{ lorder }}
            </span>
          </p>
          <p>{{ section.sectionDescription }}</p>
        </div>
        <div class="playbill-image">
          <img :src="section.sectionPlaybill" alt="">
        </div>
      </div>

      <div class="post-pad">
        <div class="post-filter">
          <el-button @click="onTypeChanged(null)" :class="{ 'type-selected': typeId == null }">全部</el-button>
          <template v-for="postTypeInfo in postTypeInfos">
            <el-button :class="{ 'type-selected': typeId == postTypeInfo.Id }" @click="onTypeChanged(postTypeInfo)">{{
              postTypeInfo.PostTypeName }}</el-button>
          </template>


        </div>
        <div class="post-head">
          <table>
            <tr>
              <td colspan="2">
                <span>
                  <a href="" style="margin-right: 10px;">全部</a>
                  <a href="" style="margin-right: 10px;">精华</a>
                  <a href="" style="margin-right: 10px;">重要</a>
                  <a href="" style="margin-right: 10px;">热帖</a>
                </span>
              </td>
              <td style="width: 105px;">作者</td>
              <td style="width: 60px;">回复/查看</td>
              <td style="width: 105px;">最后发表</td>
            </tr>
          </table>
        </div>
        <div class="post-list">
          <table>
            <tr>
              <td style="width: 30px;">asd</td>
              <td style="font-size: 14px;">asdasdasdas</td>
              <td style="width: 105px;">asdasd</td>
              <td style="width: 60px;">asdasd</td>
              <td style="width: 105px;">asdas</td>
            </tr>
            <tr class="tr-mianlist">
              <td style="width: 30px; "></td>
              <td colspan="4">版块主题</td>
            </tr>
            <tr v-if="receiveMsgs != null && receiveMsgs.sectionId == section.id">
              <td @click="onRefresh" colspan="5">有新的发帖，请点击刷新</td>
            </tr>
            <tr v-for="post in posts">
              <td style="width: 30px;"><img src="https://www.qubcedu.com/img/bbsImages/folder_common.gif" alt=""></td>
              <td style="font-size: 14px;">
                <RouterLink :to="`/postDetail/${post.id}/1`">{{ post.postTitle }}</RouterLink>
              </td>
              <td style="width: 105px;">
                <p>{{ post.createUserName }}</p>
                <p>{{ transDate(post.createDate) }}</p>
              </td>
              <td style="width: 60px;">
                <p>{{ post.replyTimes }}</p>
                <p>{{ post.browseTimes }}</p>
              </td>
              <td style="width: 105px;">
                <p>{{ post.lastReplyUserName }}</p>
                <p>{{ transDate(post.lastReplyDate) }}</p>
              </td>
            </tr>
          </table>
        </div>
        <div class="page">
          <el-pagination background layout="prev, pager, next" @current-change="onPageChanged" :total="count"
            :default-page-size="1" :current-page="currentPage" />
        </div>
      </div>

      <div class="send-pad">
        <p class="send-pad-title">
          快速发帖
        </p>
        <el-select v-model="typeSelected" class="m-2" placeholder="请选择">
          <el-option v-for="item in postType" :key="item.id" :label="item.postTypeName" :value="item.id" />
        </el-select>
        <el-input v-model="txtPostTitle" placeholder="Please input">
          <template #prepend>主题</template>
        </el-input>
        <div id="editor" style="border: 1px solid #ccc;">
          <Toolbar :editor="editorRef" style="border-bottom:1px solid #eee;" />
          <Editor style="height: 300px;" v-model="valueHtml" @onCreated="onCreated" />
        </div>
        <el-button type="primary" @click="onSend">发帖</el-button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import BBSNavigate from '@/components/bbs_components/BBSNavigate.vue';
import {AddPost, getPostType, getPosts, getSectionInfo} from '@/httpRequests/PostListRequest';
import router from '@/router';
import {onMounted} from 'vue';
import {ref} from 'vue';
import {useRoute} from 'vue-router';
import { transDate } from "@/utility/common";
import { Editor, Toolbar } from '@wangeditor/editor-for-vue'
import { shallowRef } from 'vue';
import "@wangeditor/editor/dist/css/style.css"
import { receiveMsgs } from "@/utility/signalr";

const navigates = ref([{name: '社区论坛', url: '/section'}]);

let sectionId: string = '';
const currentPage = ref(1)
const typeId = ref<any>(null)
const section = ref();
const postType = ref();
const postTypeInfos = ref()
const route = useRoute();
const posts = ref()
const count = ref(0)
const typeSelected = ref("");
const txtPostTitle = ref("");
const editorRef = shallowRef()
const valueHtml = ref("");

const onCreated = (editor: any) => {
  editorRef.value = editor
}

const onPageChanged = (pageIndex: number) => {
  if (typeId.value != null) {
    router.push(`/postList/${sectionId}/${pageIndex}?tid=${typeId.value}`)
  }else{
    router.push(`/postList/${sectionId}/${pageIndex}`);
  }
}

const onRefresh = () => {
  history.go(0);
}

const onSend = async () => {
  // var html = editor.txt.html();
  // 这里需要做一个非空判断
  let postTitle = txtPostTitle.value;
  let postTypeId = typeSelected.value;
  let replyContent = valueHtml.value;
  let areaId = section.value.areaId;
  let areaName = section.value.areaName;
  let sectionId = section.value.id;
  let sectionName = section.value.sectionTitle;
  var res = await AddPost({
    postTitle,
    areaId,
    areaName,
    sectionId,
    sectionName,
    postTypeId,
    replyCreate: {
      replyContent,
      sectionId,
    }
  })
}

onMounted(async () => {
  let page: number = parseInt(route.params["page"] as string)
  sectionId = route.params['sid'] as string;

  currentPage.value = page
  typeId.value = route.query["tid"] as string | null
  section.value = await getSectionInfo(sectionId);

  postTypeInfos.value = JSON.parse(section.value.postTypeInfo)

  let listInfo = await getPosts({
    sectionId,
    typeId: typeId.value,
    pageIndex: page,
    pageSize: 30
  })

  postType.value = await getPostType(sectionId);

  posts.value = listInfo.list;
  count.value = listInfo.count;
});

const onTypeChanged = (postTypeInfo: any) => {
  if (postTypeInfo != null) {
    router.push(`/postList/${sectionId}/${currentPage.value}?tid=${postTypeInfo.Id}`)
  } else {
    router.push(`/postList/${sectionId}/${currentPage.value}`)
  }
}
</script>

<style scoped lang="scss">
.bbs-pad {
  width: 100%;
}

.playbill {
  border: 1px solid #ccc;
  padding: 5px 10px;

  .playbill-image {
    img {
      width: 100%;
      height: 398px;
    }
  }
}

.post-pad {
  .page {
    overflow: hidden;
    padding: 16px 0;

    .el-pagination {
      float: right;
    }
  }

  .post-filter {
    margin: 10px 0;

    .type-selected {
      color: #3a8ee6;
      border-color: #3a8ee6;
      background-color: #ecf5ff;
      outline: 0;
    }
  }

  .post-head {
    background-color: #eee;

    table {
      width: 100%;
      font-size: 12px;
      border-collapse: collapse;
    }
  }

  .post-list {
    table {
      border-collapse: collapse;
      width: 100%;
      font-size: 12px;

      tr {
        height: 38px;
      }

      tr.tr-mianlist {
        td {
          border-top: 1px solid rgba(0, 0, 0, .16);
          height: 28px;
          background-color: rgba(0, 0, 0, .06);
        }
      }
    }
  }
}

.send-pad {
  border: 1px solid #ccc;
  padding: 20px;

  .send-pad-title {
    background-color: #eee;
    height: 36px;
    line-height: 36px;
  }

  .el-select {
    z-index: 2;
    width: 10%;
  }

  .el-input {
    width: 30%;
    margin-left: 30px;
  }

  #editor {
    position: relative;
    z-index: 1;
  }
}
</style>
