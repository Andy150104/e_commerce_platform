<template>
    <div
      id="loading"
      :class="[ 
        'fixed inset-0 flex justify-center items-center z-[10001] transition-all',
        showLoading ? 'block fade-in' : 'hidden fade-out',
        showLoadingBackground ? 'bg-black bg-opacity-20 pointer-events-auto' : ''
      ]"
    >
      <!-- Spinner -->
      <div class="w-12 h-12 border-4 border-blue-500 border-solid rounded-full border-t-transparent animate-spin"></div>
    </div>
  </template>
  
  <script lang="ts" setup>
  import { useLoadingStore } from '@PKG_SRC/stores/Modules/usercontrol/loadingStore';
  import { computed, watch, onMounted } from 'vue';
  
  const loadingStore = useLoadingStore();
  const showLoading = computed(() => loadingStore.showLoading);
  const showLoadingBackground = computed(() => loadingStore.showLoadingBackground);
  
  let isLongLoading = false;
  
  watch(
    () => showLoading.value,
    (newNum: number, oldNum: number) => {
      if (!(newNum === 1 && oldNum === 0) && newNum !== 0) return;
  
      if (newNum > 0) {
        isLongLoading = true;
        setTimeout(() => {
          if (isLongLoading) loadingStore.SetShowLoadingBackground(true);
        }, 500);
      } else {
        isLongLoading = false;
        setTimeout(() => {
          if (showLoadingBackground.value) loadingStore.SetShowLoadingBackground(false);
        }, 500);
      }
    }
  );
  
  onMounted(() => {
    loadingStore.showLoading = 1;
    setTimeout(() => {
      loadingStore.showLoading = 0;
    }, 500);
  });
  </script>
  
  <style scoped>
  @tailwind base;
  @tailwind components;
  @tailwind utilities;
  
  @layer utilities {
    @keyframes fadeIn {
      0% {
        opacity: 0;
        transform: scale(0.9);
      }
      100% {
        opacity: 1;
        transform: scale(1);
      }
    }
  
    @keyframes fadeOut {
      0% {
        opacity: 1;
        transform: scale(1);
      }
      100% {
        opacity: 0;
        transform: scale(0.9);
      }
    }
  
    @keyframes spinSmooth {
      0% {
        transform: rotate(0deg);
      }
      50% {
        transform: rotate(180deg);
        animation-timing-function: ease-in;
      }
      100% {
        transform: rotate(360deg);
        animation-timing-function: ease-out;
      }
    }
  
    .fade-in {
      animation: fadeIn 0.6s cubic-bezier(0.25, 0.8, 0.25, 1) forwards;
    }
  
    .fade-out {
      animation: fadeOut 0.6s cubic-bezier(0.25, 0.8, 0.25, 1) forwards;
    }
  
    .animate-spin {
      animation: spinSmooth 1.2s infinite;
    }
  }
  </style>
  