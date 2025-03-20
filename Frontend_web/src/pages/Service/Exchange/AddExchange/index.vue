<template>
  <BaseScreenProduct>
    <template #body>
      <div v-if="successState" class="flex flex-col items-center justify-center min-h-screen">
        <div class="p-6 bg-white rounded-lg shadow-lg text-center">
          <h1 class="text-2xl font-bold text-green-600">Exchange Product Created Successfully!</h1>
          <p class="text-gray-700 mt-2">Do you want to go back?</p>
          <div class="mt-4 flex space-x-4">
            <button @click="ClearAll" class="bg-gray-500 text-white px-4 py-2 rounded-lg hover:bg-gray-600">Go Back</button>
          </div>
        </div>
      </div>

      <div v-else id="gradient-card" class="animate-fade-left p-10 w-full max-w-2xl mx-auto rounded-xl mb-16 shadow-lg">
        <div class="max-w-3xl mx-auto text-center">
          <h1 class="text-4xl font-bold text-black dark:text-white mb-4">Add Exchange Product</h1>
          <p class="text-lg text-gray-700 mb-8">Create your Exchange Product</p>
        </div>

        <div class="grid grid-cols-1 gap-8">
          <div class="space-y-4 xl:col-span-2">
            <UserControlTextFieldLabel
              :xml-column="xmlColumns.name"
              :maxlength="50"
              :disabled="false"
              :err-msg="fieldErrors.name"
              placeholder="Enter your Product name"
            />
            <UserControlTextFieldLabel
              :xml-column="xmlColumns.description"
              :maxlength="200"
              :disabled="false"
              :err-msg="fieldErrors.description"
              placeholder="Product description"
            />

            <UserControlUploadImage
              :max-number-image="6"
              :is-show-popover="false"
              :label="'Upload up to 6 images'"
              :disabled="uploadImageStore.uploadImage.length > 6"
            />

            <p v-if="uploadImageStore.uploadImage.length > 6" class="text-red-500 text-sm">You can only upload up to 6 images.</p>

            <div class="flex space-x-4">
              <button @click="ClearAll" class="w-full bg-gray-500 text-white p-3 rounded-lg hover:bg-gray-600">Clear inputs</button>
              <button @click="AddExchange" class="w-full bg-blue-500 text-white p-3 rounded-lg hover:bg-blue-600">Add product!</button>
            </div>
          </div>
        </div>
      </div>
    </template>
  </BaseScreenProduct>
</template>

<script lang="ts" setup>
  import { ref } from 'vue'
  import { storeToRefs } from 'pinia'
  import { useForm } from 'vee-validate'
  import BaseScreenProduct from '@PKG_SRC/layouts/Basecreen/BaseScreenProduct.vue'
  import UserControlTextFieldLabel from '@PKG_SRC/components/UserControl/UserControlTextFieldLabel.vue'
  import UserControlUploadImage from '@PKG_SRC/components/UserControl/UserControlUploadImage.vue'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import { useAddExchangeStore } from '@PKG_SRC/stores/Modules/Blind_Box/AddExchangeStore'
  import { useUploadImageStore } from '@PKG_SRC/stores/Modules/usercontrol/uploadImageStore'

  const uploadImageStore = useUploadImageStore()
  const store = useAddExchangeStore()
  const successState = ref(false)

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
  }

  const AddExchange = async () => {
    await store.addExchangeProduct()
    successState.value = true
  }
</script>
