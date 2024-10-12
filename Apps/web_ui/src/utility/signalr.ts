import { HubConnectionBuilder } from '@microsoft/signalr';
import { ref } from 'vue';

export const receiveMsgs = ref<any>();

export const connection = new HubConnectionBuilder()
  .withUrl('http://124.222.216.19:5002/bbs/signalr-hubs/messaging')
  .build();

connection.on('onReceiveMessage', (res) => {
  receiveMsgs.value = res;
});

connection.start().catch();
