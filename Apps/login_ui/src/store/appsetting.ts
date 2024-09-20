import { defineStore } from "pinia";

export const useAppsetting = defineStore('appsetting', {
    state: () => {
        return {
            apiRootKey: ''
        }
    }
})