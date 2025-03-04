<template>
  <div class="bg-white flex flex-col min-h-screen dark:bg-gray-900">
    <!-- Header -->
    <header class="sticky top-0 z-40 w-full">
      <NavBar />
    </header>
    <!-- Body -->
    <div class="max-w-8xl mt-0 mx-0 p-2 md:mt-4 md:p-8 shadow-lg md:mx-4 min-h-[calc(100vh-64px)]">
      <slot name="body"></slot>
    </div>
    <FooterControl />
    <Loading />
    <MessageToast />
    <ScrollToTopBtn />
  </div>
</template>
<script lang="ts" setup>
  import Loading from '@PKG_SRC/components/UserControl/Loading.vue'
  import MessageToast from '@PKG_SRC/components/Toast/MessageToast.vue'
  import FooterControl from '@PKG_SRC/components/Footer/FooterControl.vue'
  import ScrollToTopBtn from '../Btn/ScrollToTopBtn.vue'
  import NavBar from '@PKG_SRC/components/NavBar/NavBar.vue'
  import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'

  const formMessageStore = useFormMessageStore()
</script>
