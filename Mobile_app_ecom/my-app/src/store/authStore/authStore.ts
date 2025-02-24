// src/store/useAuthStore.ts
import { create } from 'zustand';
import { persist, createJSONStorage } from 'zustand/middleware';
import axios from 'axios';
import { authServerApi } from "../../api/useHttpServer";
import { ContentType } from '../../api/ServerApi/authApi';
import AsyncStorage from '@react-native-async-storage/async-storage';

export type TokenResponse = {
  access_token: string
  token_type: string
  expires_in: number
  scope: string
  refresh_token: string
}

interface AuthState {
  isAuthorization: boolean;
  accessToken: string;
  refreshToken: string;
  expiresIn: number;
  scope: string;
  loading: boolean;
  error?: string;
  login: (userId: string, password: string, userNameOrEmail: string) => Promise<void>;
  logout: () => Promise<void>;
  setTokens: (tokens: TokenResponse) => void;
  resetStore: () => void;
  getRefreshToken: () => Promise<void>;
}

export const useAuthStore = create<AuthState>()(
  persist(
    (set, get) => ({
      isAuthorization: false,
      accessToken: '',
      refreshToken: '',
      expiresIn: 0,
      scope: '',
      loading: false,
      error: undefined,

      // Reset store to default
      resetStore: () => {
        set({
          isAuthorization: false,
          accessToken: '',
          refreshToken: '',
          expiresIn: 0,
          scope: '',
          error: undefined,
        });
      },

      // Set tokens after successful login/refresh
      setTokens: (tokens: TokenResponse) => {
        set({
          isAuthorization: true,
          accessToken: tokens.access_token,
          refreshToken: tokens.refresh_token,
          expiresIn: tokens.expires_in,
          scope: tokens.scope,
        });
      },

      // Login action
      login: async (userId, password, userNameOrEmail) => {
        set({ loading: true, error: undefined });

        try {
          const authParams = new URLSearchParams();
          authParams.append('grant_type', 'password');
          authParams.append('client_id', 'service_client');
          authParams.append('client_secret', 'SWD392-LamNN15-GROUP3-SPRING2025'); // Replace with your env
          authParams.append('username', userId);
          authParams.append('password', password);
          authParams.append('UserNameOrEmail', userNameOrEmail);

          const res = await authServerApi.request({
            path: "/connect/token",
            method: "POST",
            type: ContentType.UrlEncoded,
            body: authParams.toString(),
            secure: false,
          });

          const data = res.data as TokenResponse;

          if (!data.access_token) {
            throw new Error("Không tìm thấy access_token trong response");
          }

          // Set tokens after successful login
          get().setTokens(data);
        } catch (error) {
          console.error('Login error:', error);
          set({ error: "Đăng nhập thất bại!" });
        } finally {
          set({ loading: false });
        }
      },

      // Logout action
      logout: async () => {
        get().resetStore(); // Xóa tokens và trạng thái
      },

      getRefreshToken: async () => {
      },
    }),
    {
      name: 'auth-storage', // Storage key
      storage: createJSONStorage(() => AsyncStorage), // Use sessionStorage, change to localStorage if needed
    }
  )
);
