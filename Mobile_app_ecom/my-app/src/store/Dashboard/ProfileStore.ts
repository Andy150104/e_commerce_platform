// src/store/useProfileStore.ts
import { create } from 'zustand';
import { clientApi } from '../../api/useHttpClient';
import { UDSSelectUserProfileEntity } from '../../api/ClientApi/clientApi';

interface ProfileState {
  profile?: UDSSelectUserProfileEntity;
  loading: boolean;
  error?: string;
  fetchUserProfile: () => Promise<void>;
}

export const useProfileStore = create<ProfileState>((set) => ({
  profile: undefined,
  loading: false,
  error: undefined,

  fetchUserProfile: async () => {
    set({ loading: true, error: undefined });

    try {
      const res = await clientApi.api.udsSelectUserProfilePost(
        { isOnlyValidation: false },
      );

      if (res.data.success) {
        set({ profile: res.data.response, loading: false });
      } else {
        set({ error: res.data.message, loading: false });
      }
    } catch (error) {
      set({ error: "Không thể lấy thông tin người dùng", loading: false });
    }
  },
}));
