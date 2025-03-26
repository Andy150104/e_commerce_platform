<template>
  <BaseScreenDashBoard>
    <template #body>
      <div>
        <ChatMany />
      </div>
    </template>
  </BaseScreenDashBoard>
</template>
<script setup lang="ts">
  import ChatMany from '@PKG_SRC/components/Chat/ChatMany.vue'
  import BaseScreenDashBoard from '@PKG_SRC/layouts/Basecreen/BaseScreenDashBoard.vue'
</script>
