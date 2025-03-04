<template>
  <Loading />
  <div
    class="flex items-center justify-center min-h-screen h-screen bg-black bg-opacity-80 bg-cover bg-center z-10 overflow-auto"
    style="background-image: url('https://www.elle.vn/app/uploads/2024/09/05/606140/labubu-trong-bo-suu-tap-macaron-.jpeg')"
  >
    <div class="absolute inset-0 bg-slate-900 bg-opacity-30 z-10 h-full"></div>
    <div
      class="bg-white my-8 w-full h-full dark:bg-gray-800 shadow-lg rounded-lg p-6 md:mx-auto md:h-auto md:w-3/4 md:p-2 md:px-9 3xl:w-[1200px] 3xl:p-6 sm:p-16 z-20 overflow-y-auto max-h-[800px] min-h-96"
    >
      <slot name="body"></slot>
    </div>
    <MessageToast />
  </div>
</template>
<script lang="ts" setup>
  import MessageToast from '@PKG_SRC/components/Toast/MessageToast.vue'
  import Loading from '@PKG_SRC/components/UserControl/Loading.vue'
  import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'

  const formMessageStore = useFormMessageStore()
</script>
<style scoped>
  slot[name='body'] {
    padding-bottom: 1rem;
  }
</style>
