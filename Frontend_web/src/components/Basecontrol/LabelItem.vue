<!--共通部品:ラベル-->
<template>
  <label :class="classLabel">{{ xmlColumn.name }}</label>
</template>
<script setup lang="ts">
  import { className } from '@PKG_SRC/utils/class/className'
  import type { xmlColumn } from '@PKG_SRC/utils/xml'
  import { type PropType } from 'vue'
  const props = defineProps({
    xmlColumn: {
      type: Object as PropType<xmlColumn>,
      required: true,
    },
    classLabel: {
      type: String,
      required: false,
      default: className.LABEL_DEFAULT,
    },
  })
</script>
