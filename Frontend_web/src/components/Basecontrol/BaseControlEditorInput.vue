<template>
  <div class="mb-3">
    <LabelItem :xml-column="xmlColumn" />
    <Editor ref="editorRef" v-model="internalValue" :editorConfig="editorConfig" editorStyle="height: 320px">
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
          <button v-tooltip.bottom="'Image'" @click="customImageHandler">
            <p class="text-xs">Image</p>
          </button>
        </span>
      </template>
    </Editor>
  </div>
</template>

<script lang="ts" setup>
  import { ref, watch } from 'vue'
  import Editor from 'primevue/editor'
  import type { xmlColumn } from '@PKG_SRC/utils/xml'
  import LabelItem from './LabelItem.vue'
  import { useUploadImageStore } from '@PKG_SRC/stores/Modules/usercontrol/uploadImageStore'

  const store = useUploadImageStore()

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
  const editorRef = ref<any>()
  const internalValue = ref(props.modelValue)

  function customImageHandler() {
    const quill = editorRef.value?.quill
    if (!quill) return

    const input = document.createElement('input')
    input.setAttribute('type', 'file')
    input.setAttribute('accept', 'image/*')
    input.click()
    input.onchange = async () => {
      const file = input.files?.[0]
      if (!file) return
      const reader = new FileReader()
      reader.onload = async (e) => {
        try {
          const base64 = e.target?.result as string
          const range = quill.getSelection(true)
          const loadingUrl = 'https://upload.wikimedia.org/wikipedia/commons/b/b1/Loading_icon.gif'
          quill.insertEmbed(range.index, 'image', loadingUrl, 'user')
          const url = await store.uploadCloudinaryBase64(base64)
          quill.deleteText(range.index, 1)
          quill.insertEmbed(range.index, 'image', url, 'user')
        } catch (error) {
          console.error('Error uploading image:', error)
        }
      }
      reader.readAsDataURL(file)
    }
  }

  const editorConfig = {
    modules: {
      toolbar: {
        // Thêm 'image' vào mảng container để hiển thị nút hình ảnh trên cùng 1 dòng
        container: [['bold', 'italic', 'underline'], [{ header: [1, 2, 3, false] }], [{ font: [] }]],
      },
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
