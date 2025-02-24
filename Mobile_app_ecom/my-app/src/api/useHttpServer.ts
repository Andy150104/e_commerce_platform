// file: src/apiClient.ts

import { Api } from "./ServerApi/authApi";

export const authServerApi = new Api({
  baseURL: "http://192.168.50.199:5090/",
});
