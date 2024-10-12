import { computed } from "vue";
import { useRoute } from "vue-router";

export function getCurrentComponentName(): string | symbol | null | undefined {
    const route = useRoute();
    const { name } = route;
    var routeName = name;
    
    routeName = computed(() => {
        switch (name) {
            case "PostList":
            case "PostDetail":
                return "Section"
            case "FileList":
                return "FileType"
            default:
                return name
        }
    }).value
    return routeName;
}