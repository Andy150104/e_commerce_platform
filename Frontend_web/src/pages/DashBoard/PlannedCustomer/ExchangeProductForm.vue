<template>
    <Dialog v-model:visible="visible" :modal="true" :closable="true" header="Add Exchange Product" :style="{ width: '50vw' }">
      <div v-if="successState" class="flex flex-col items-center justify-center min-h-screen">
        <div class="p-6 bg-white rounded-lg shadow-lg text-center">
          <h1 class="text-2xl font-bold text-green-600">Exchange Product Created Successfully!</h1>
        </div>
      </div>
      <div v-if="failedState" class="flex flex-col items-center justify-center min-h-screen">
        <div class="p-6 bg-white rounded-lg shadow-lg text-center">
          <h1 class="text-2xl font-bold text-red-600">Exchange Product Created Failed!</h1>
        </div>
      </div>
      <div v-else class="p-6">
        <div class="max-w-3xl mx-auto text-center">
          <h1 class="text-2xl font-bold text-black mb-4  dark:text-white ">Add Exchange Product</h1>
          <p class="text-lg text-gray-700 mb-6">Create your Exchange Product</p>
        </div>
  
        <div class="grid grid-cols-1 gap-6">
          <UserControlTextFieldLabel
            :xml-column="xmlColumns.name"
            :maxlength="50"
            :err-msg="fieldErrors.name"
            placeholder="Enter your Product name"
          />
          <BaseControlEditorInput :xml-column="xmlColumns.description" v-model="editorValue" />
          <UserControlUploadImage :max-number-image="6" :is-show-popover="false" :label="'Upload up to 6 images'" />
  
          <div class="flex space-x-4">
            <button @click="ClearAll" class="w-full bg-gray-500 text-white p-3 rounded-lg hover:bg-gray-600">Clear inputs</button>
            <button @click="AddExchange" class="w-full bg-blue-500 text-white p-3 rounded-lg hover:bg-blue-600">Add product!</button>
          </div>
        </div>
      </div>
    </Dialog>
  </template>
  
  <script setup>
  import { ref } from 'vue'
  import { storeToRefs } from 'pinia'
  import { useForm } from 'vee-validate'
  import UserControlTextFieldLabel from '@PKG_SRC/components/UserControl/UserControlTextFieldLabel.vue'
  import UserControlUploadImage from '@PKG_SRC/components/UserControl/UserControlUploadImage.vue'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import { defineEmits } from 'vue';
  import BaseControlEditorInput from '@PKG_SRC/components/Basecontrol/BaseControlEditorInput.vue'
  import { useAddExchangeStore } from '@PKG_SRC/stores/Modules/Blind_Box/AddExchangeStore'
  import { useUploadImageStore } from '@PKG_SRC/stores/Modules/usercontrol/uploadImageStore'

  const emit = defineEmits(['added']);
  const uploadImageStore = useUploadImageStore()
  const store = useAddExchangeStore()
  const successState = ref(false)
  const failedState = ref(false)
  const editorValue = ref('')
  const visible = ref(false) // Điều khiển hiển thị popup
  const { fieldValues, fieldErrors } = storeToRefs(store)
  const formContext = useForm({ initialValues: fieldValues.value })
  store.SetFields(formContext)
  const xmlColumns = {
    name: XmlLoadColumn({ id: 'name', name: 'Name', rules: 'required', visible: true, option: '' }),
    description: XmlLoadColumn({ id: 'description', name: 'Description', rules: 'required', visible: true, option: '' }),
  }
  
  const ClearAll = () => {
    store.ResetStore()
    uploadImageStore.uploadImage = []
    successState.value = false
    failedState.value = false
    visible.value = false // Đóng popup sau khi xóa
  }
  
  const AddExchange = async () => {
    store.fields.setFieldValue('description',editorValue.value)
    const success = await store.addExchangeProduct();
  if (success) {
    emit('added'); // Emit event nếu thêm thành công
    successState.value = true;
  }else{
    failedState.value = true;
  }
  }
  
  // Hàm này để mở popup từ component cha
  const open = () => {
    visible.value = true
    failedState.value = false
    successState.value = false
  }
  
  // Xuất hàm `open` để gọi từ bên ngoài
  defineExpose({ open })
  </script>
  