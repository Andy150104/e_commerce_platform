<template>
    <div class="mb-3">
    <LabelItem :xml-column="xmlColumn"/>
      <Editor v-model="internalValue" :editorConfig="editorConfig" editorStyle="height: 320px">
        <template v-slot:toolbar>
          <span class="ql-formats">
            <button v-tooltip.bottom="'Bold'" class="ql-bold"></button>
            <button v-tooltip.bottom="'Italic'" class="ql-italic"></button>
            <button v-tooltip.bottom="'Underline'" class="ql-underline"></button>
            <select v-tooltip.bottom="'Heading'" class="ql-header">
              <option selected></option>
              <option value="1">Heading 1</option>
              <option value="2">Heading 2</option>
              <option value="3">Heading 3</option>
            </select>
            <select class="ql-font">
              <option selected></option>
              <option value="monospace">Monospace</option>
            </select>
          </span>
        </template>
      </Editor>
    </div>
  </template>
  
  <script lang="ts" setup>
  import { ref } from 'vue'
  import Editor from 'primevue/editor'
  import type { xmlColumn } from '@PKG_SRC/utils/xml'
  import LabelItem from './LabelItem.vue'
  
  const props = defineProps({
    modelValue: {
      type: String,
      default: '',
    },
    xmlColumn: {
      type: Object as PropType<xmlColumn>,
      required: true,
    },
  })
  const emit = defineEmits(['update:modelValue'])
  
  const internalValue = ref(props.modelValue)
  
  const editorConfig = {
    modules: {
      toolbar: [
        ['bold', 'italic', 'underline'],
        [{ 'header': [1, 2, 3, false] }], // Cấu hình heading
        [{ 'font': [] }],
      ],
    },
  }
  
  watch(
    () => props.modelValue,
    (newVal) => {
      internalValue.value = newVal
    }
  )
  
  watch(internalValue, (newVal) => {
    emit('update:modelValue', newVal)
  })
  </script>
