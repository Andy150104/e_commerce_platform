// App.tsx
import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Text, View } from 'react-native';
import AppNavigator from './navigation/AppNavigator';
import { useEffect } from 'react';
import AsyncStorage from '@react-native-async-storage/async-storage';

export default function App() {
  useEffect(() => {
    AsyncStorage.getAllKeys().then((keys) => {
      console.log('All keys:', keys);
      keys.forEach(async (key) => {
        const value = await AsyncStorage.getItem(key);
        console.log(`Key: ${key}, Value: ${value}`);
      });
    });
  }, []);
  return (
    <>
      <AppNavigator />
      <StatusBar style="auto" />
    </>
  );
}
