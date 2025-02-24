import { useAuthStore } from "../store/authStore/authStore";
import { Api } from "./ClientApi/clientApi";

export const clientApi = new Api({
  baseURL: "http://192.168.50.199:5092",
  securityWorker: () => {
    const { accessToken } = useAuthStore.getState();
    if (!accessToken) return {};

    return {
      headers: {
        Authorization: `Bearer ${accessToken}`,
      },
    };
  },
});
