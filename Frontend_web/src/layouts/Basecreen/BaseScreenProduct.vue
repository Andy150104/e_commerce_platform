<template>
  <div class="bg-white flex flex-col min-h-screen dark:bg-gray-900">
    <!-- Header -->
    <header class="sticky top-0 z-40 w-full">
      <NavBar />
    </header>
    <!-- Body -->
    <div
      ref="scrollContainer"
      class="max-w-8xl shadow-[0_4px_10px_4px_rgba(0,0,0,0.08)] rounded-lg mt-0 mx-0 p-4 md:mt-4 mb-6 pt-8 md:mx-10 min-h-[calc(100vh-64px)] overflow-y-auto"
      @scroll.passive="emit('on-scroll-load')"
    >
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

  interface Emits {
    (e: 'on-scroll-load'): void
  }

  const emit = defineEmits<Emits>()
</script>
