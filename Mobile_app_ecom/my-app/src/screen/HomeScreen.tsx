// src/screen/HomeScreen.tsx
import React, { useEffect } from 'react';
import { View, Text, ActivityIndicator, ScrollView, Button } from 'react-native';
import tw from '../utils/tailwind';
import { useAuthStore } from '../store/authStore/authStore';
import { useProfileStore } from '../store/Dashboard/ProfileStore';

const HomeScreen: React.FC = () => {
  const { accessToken, logout } = useAuthStore();
  const { profile, loading, error, fetchUserProfile } = useProfileStore();

  useEffect(() => {
    fetchUserProfile();
  }, []);

  return (
    <ScrollView contentContainerStyle={tw`p-4`}>
      <Text style={tw`text-2xl font-bold`}>Welcome to Home!</Text>
      <Text style={tw`mt-2`}>Your Access Token:</Text>
      <Text style={tw`text-xs mt-1 text-gray-500`}>{accessToken}</Text>

      <View style={tw`mt-4`}>
        {loading ? (
          <ActivityIndicator size="large" />
        ) : error ? (
          <Text style={tw`text-red-500 text-center`}>{error}</Text>
        ) : profile ? (
          <View>
            <Text style={tw`text-xl font-bold mb-4`}>Thông tin người dùng</Text>
            <Text>Email: {profile.email || 'Chưa cập nhật'}</Text>
            <Text>Họ: {profile.firstName || 'Chưa cập nhật'}</Text>
            <Text>Tên: {profile.lastName || 'Chưa cập nhật'}</Text>
            <Text>Số điện thoại: {profile.phoneNumber || 'Chưa cập nhật'}</Text>
            <Text>Ngày sinh: {profile.birthDate || 'Chưa cập nhật'}</Text>
            <Text>
              Giới tính:{" "}
              {profile.gender != null
                ? profile.gender === 1
                  ? 'Nam'
                  : 'Nữ'
                : 'Chưa cập nhật'}
            </Text>
            <Text>Hình ảnh: {profile.imageUrl || 'Chưa cập nhật'}</Text>
          </View>
        ) : (
          <Text>Không có thông tin người dùng.</Text>
        )}
      </View>
      
      <View style={tw`mt-6`}>
        <Button title="Logout" onPress={logout} />
      </View>
    </ScrollView>
  );
};

export default HomeScreen;
