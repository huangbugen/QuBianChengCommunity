<template>
  <div>
    <BBSNavigate :navigates="navigates"></BBSNavigate>
    <div class="reply-pad">
      <div class="reply-item" v-for="reply in replys">
        <div class="reply-pad-left" v-for="user in getUserById(reply.userId)">
          <p>{{ user.userName }}</p>
          <img
            :src="reply.headUrl"
            alt=""
            style="width: 120px; height: 120px; padding: 5px" />
          <div class="post-count">
            <div class="post-count-item">
              <p>{{ user.postCount }}</p>
              <p>主题</p>
            </div>
            <div class="post-count-item">
              <p>{{ user.replyCount }}</p>
              <p>帖子</p>
            </div>
            <div class="post-count-item">
              <p>{{ user.currentIntegral }}</p>
              <p>积分</p>
            </div>
          </div>
          <p>{{ user.levelName }}</p>
          <el-progress
            :show-text="false"
            :percentage="
              getPercentage(user.currentIntegral, user.nextIntegral)
            " />
        </div>
        <div class="reply-pad-right">
          <div class="quote-pad" v-html="reply.quoteReplyContent"></div>
          <div class="text" v-html="reply.replyContent"></div>
          <div class="reply-edit">
            <span @click="onReply(reply)">回复</span>
          </div>
        </div>
      </div>
    </div>
    <div class="page">
      <el-pagination
        background
        layout="prev, pager, next"
        @current-change="onPageChanged"
        :total="count"
        :default-page-size="1"
        :current-page="currentPage" />
    </div>

    <div class="send-pad">
      <p class="send-pad-title">快速回帖</p>
      <div
        class="quote"
        ref="quotePad"
        v-if="post != null && quoteInfo.quoteContent != null">
        <div class="quote-title">RE:{{ post.postTitle }}</div>
        <div class="quote-title">
          本帖由 {{ quoteInfo.quoteUserName }} 发表于
          {{ transDate(quoteInfo.quoteTime) }}
        </div>
        <div class="quote-content" v-html="quoteInfo.quoteContent"></div>
      </div>
      <div id="editor" style="border: 1px solid #ccc">
        <Toolbar :editor="editorRef" style="border-bottom: 1px solid #eee" />
        <Editor
          style="height: 300px"
          v-model="valueHtml"
          @onCreated="onCreated" />
      </div>
      <el-button type="primary" @click="onSend">回帖</el-button>
    </div>
  </div>
</template>

<script setup lang="ts">
import BBSNavigate from '@/components/bbs_components/BBSNavigate.vue';
import { ref, shallowRef } from 'vue';
import { useRoute } from 'vue-router';
import {
  getPosts,
  getReplies,
  addReplies,
} from '@/httpRequests/PostDetailRequest';
import { getQuoteText } from '@/utility/DomHelper';
import { transDate } from '@/utility/common';
import { Toolbar, Editor } from '@wangeditor/editor-for-vue';
import '@wangeditor/editor/dist/css/style.css';
import { onMounted } from 'vue';

const navigates = ref([{ name: '社区论坛', url: '/section' }]);

const post = ref();
const replys = ref();
const users = ref();
const route = useRoute();
const currentPage = ref(1);
const percentage = ref(0);
const count = ref(1);
const editorRef = shallowRef();
const valueHtml = ref('');
const quoteInfo = ref<any>({});
const quotePad = ref();

onMounted(async () => {
  let res = await getPosts(postId);
  post.value = res.post;
  replys.value = res.replys;
  users.value = res.users;

  navigates.value.push(
    {
      name: `${post.value.sectionName}`,
      url: `/postList/${post.value.sectionId}/1`,
    },
    { name: `${post.value.postTitle}`, url: `` }
  );
});

const getUserById = (userId: string) => {
  return [users.value.filter((m: any) => m.id == userId)[0]];
};

const getPercentage = (curr: number, next: number) => {
  return ((curr / next) * 100).toFixed(0);
};

const onCreated = (editor: any) => {
  editorRef.value = editor;
};

const onSend = async () => {
  var res = await addReplies({
    replyContent: valueHtml.value,
    sectionId: post.value.sectionId,
    postId: post.value.id,
    quoteReplyId: quoteInfo.value.quoteReplyId,
    quoteReplyUserId: quoteInfo.value.quoteUserId,
    quoteReplyContent: quotePad.value.innerHTML,
  });
  alert(res);
};

const onReply = async (reply: any) => {
  var resReplyQuote = getQuoteText(reply.replyContent);
  quoteInfo.value.quoteReplyId = reply.id;
  quoteInfo.value.quoteUserId = reply.userId;
  quoteInfo.value.quoteUserName = reply.userName;
  quoteInfo.value.quoteTime = reply.creationTime;
  quoteInfo.value.quoteContent = resReplyQuote;
};

const onPageChanged = async (index: number) => {
  currentPage.value = index;
  let res = await getReplies(postId, index);
  replys.value = res.replys;
  users.value = res.users;
};

let postId = route.params['pid'] as string;
</script>

<style scoped lang="scss">
.reply-pad {
  .reply-item {
    display: flex;

    .reply-pad-left {
      width: 160px;

      .post-count {
        display: flex;

        .post-count-item {
          flex: 1;
        }
      }

      :deep(.el-progress-bar__inner) {
        background-color: red;
      }
    }

    .reply-pad-right {
      flex: 1;
      .quote-pad {
        background-color: #eee;
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

.page {
  overflow: hidden;
  padding: 16px 0;

  .el-pagination {
    float: right;
  }
}
</style>
