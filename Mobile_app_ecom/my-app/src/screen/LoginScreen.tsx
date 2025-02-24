// src/screen/LoginScreen.tsx
import React, { useState, useEffect } from "react";
import { View, Text, TextInput, TouchableOpacity, ScrollView, ActivityIndicator } from "react-native";
import { LinearGradient } from "expo-linear-gradient";
import tw from "../utils/tailwind";
import { useNavigation } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { useAuthStore } from "../store/authStore/authStore";
import { RootStackParamList } from "../navigation/AppNavigator";

const LoginScreen: React.FC = () => {
  const { login, loading, error, accessToken } = useAuthStore();
  const navigation = useNavigation<NativeStackNavigationProp<RootStackParamList>>();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleSignIn = async () => {
    await login(username, password, username);
  };

  useEffect(() => {
    if (accessToken) {
      navigation.navigate("Home"); // Chuyển sang màn Home sau khi login thành công
    }
  }, [accessToken]);

  return (
    <View style={tw`flex-1`}>
      <LinearGradient colors={["#fff", "#ccc"]} style={tw`flex-1`}>
        <ScrollView contentContainerStyle={tw`flex-grow justify-center`}>
          <View style={tw`px-8`}>
            <Text style={tw`text-3xl font-bold text-center`}>Login</Text>

            <TextInput
              placeholder="Username"
              style={tw`border border-gray-300 rounded-md py-4 px-4 mb-4`}
              value={username}
              onChangeText={(val) => setUsername(val)}
            />

            <TextInput
              placeholder="Password"
              secureTextEntry
              style={tw`border border-gray-300 rounded-md py-4 px-4 mb-4`}
              value={password}
              onChangeText={(val) => setPassword(val)}
            />

            <TouchableOpacity
              style={tw`bg-indigo-600 rounded-md py-3`}
              onPress={handleSignIn}
              disabled={loading}
            >
              {loading ? (
                <ActivityIndicator color="#fff" />
              ) : (
                <Text style={tw`text-white text-center font-bold text-lg`}>Sign in</Text>
              )}
            </TouchableOpacity>

            {error && <Text style={tw`text-red-600 mt-2 text-center`}>{error}</Text>}
          </View>
        </ScrollView>
      </LinearGradient>
    </View>
  );
};

export default LoginScreen;
