import Main from '@/views/Main.vue';
import { RouteRecordRaw, createRouter, createWebHashHistory } from 'vue-router';

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'Main',
    component: Main,

    children: [
      {
        path: '/section',
        name: 'Section',
        component: () => import('../views/BBSSystem/Section.vue'),
      },
      {
        path: '/postList/:sid/:page',
        name: 'PostList',
        component: () => import('../views/BBSSystem/PostList.vue'),
      },
      {
        path: '/postDetail/:pid/:page',
        name: 'PostDetail',
        component: () => import('../views/BBSSystem/PostDetail.vue'),
      },
    ],
  },
];

const router = createRouter({
  history: createWebHashHistory(),
  routes,
});

export default router;
