<template>
  <div class="bg-white flex flex-col min-h-screen dark:bg-gray-900">
    <!-- Header -->
    <header class="sticky top-0 z-40 w-full"></header>
    <SlideBarUserNavigation>
      <template #body>
        <div
          class="max-w-8xl shadow-[0_4px_10px_4px_rgba(0,0,0,0.1)] rounded-lg mt-0 mx-0 p-4 md:mt-20 mb-10 md:p-8 md:mx-4 min-h-[calc(100vh-64px)]"
        >
          <slot name="body"></slot>
        </div>
        <FooterControl />
        <Loading />
        <ScrollToTopBtn />
        <MessageToast />
      </template>
    </SlideBarUserNavigation>
  </div>
</template>
<script setup lang="ts">
  import FooterControl from '@PKG_SRC/components/Footer/FooterControl.vue'
  import MessageToast from '@PKG_SRC/components/Toast/MessageToast.vue'
  import ScrollToTopBtn from '../Btn/ScrollToTopBtn.vue'
  import Loading from '@PKG_SRC/components/UserControl/Loading.vue'
  import SlideBarUserNavigation from '@PKG_SRC/components/NavBar/SlideBarUserNavigation.vue'
  import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'

  const formMessageStore = useFormMessageStore()
</script>
