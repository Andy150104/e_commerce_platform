<template>
  <BaseScreenProduct>
    <template #body>
      <!-- Success Message -->
      <div
        v-if="successMessage"
        class="fixed top-5 right-5 z-50 w-96 transition-opacity duration-1000 ease-out"
        :class="{ 'opacity-0': !successMessage }"
      >
        <div class="flex items-center p-4 text-sm text-green-800 rounded-lg bg-green-50 shadow-lg" role="alert">
          <svg class="flex-shrink-0 w-4 h-4 mr-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 20 20">
            <path d="M18 10A8 8 0 1 1 2 10a8 8 0 0 1 16 0ZM9 13h2v2H9v-2Zm0-6h2v4H9V7Z" />
          </svg>
          <span class="font-medium">Success!</span> added successfully.
        </div>
      </div>

      <div id="gradient-card" 
     class="animate-fade-left p-10 w-full max-w-2xl mx-auto rounded-xl mb-16 shadow-lg">
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
            <UserControlTextFieldLabel
              :xml-column="xmlColumns.price"
              :maxlength="50"
              :disabled="false"
              :err-msg="fieldErrors.price"
              placeholder="0.00"
            />

            <!-- Image Upload (Max 6) -->
            <UserControlUploadImage
              :max-number-image="6"
              :is-show-popover="false"
              :label="'Upload up to 6 images'"
              :disabled="uploadImageStore.uploadImage.length > 6"
            />

            <!-- Warning if max images reached -->
            <p v-if="uploadImageStore.uploadImage.length > 6" class="text-red-500 text-sm">You can only upload up to 6 images.</p>

            <div class="flex space-x-4">
              <button @click="ClearAll" class="w-full bg-gray-500 text-white p-3 rounded-lg hover:bg-gray-600">Clear inputs</button>
              <button @click="AddExchange" class="w-full bg-blue-500 text-white p-3 rounded-lg hover:bg-blue-600">Add product!</button>
            </div>

            <!-- Uploaded Images Preview -->
            <!-- <div class="space-y-4 xl:col-span-1">
              <h2 class="text-2xl text-black dark:text-white font-semibold">Product's images</h2>
              <p class="text-sm text-black dark:text-white">These images will be displayed publicly so be careful what you share.</p>
              <div class="w-full grid grid-cols-2 md:grid-cols-3 gap-2 md:gap-4">
                <template v-if="uploadImageStore.uploadImage.length > 0">
                  <img
                    v-for="(image, index) in uploadImageStore.uploadImage.slice(0, 6)"
                    :key="index"
                    class="rounded-lg object-cover w-full h-32 md:h-40"
                    :src="image.imagePreview"
                    alt="Uploaded Image"
                  />
                </template>
                <template v-else>
                  <div class="col-span-2 md:col-span-3 flex justify-center">
                    <img
                      class="rounded-lg object-cover w-40 h-40 md:w-64 md:h-64"
                      src="https://static.vecteezy.com/system/resources/previews/009/292/244/non_2x/default-avatar-icon-of-social-media-user-vector.jpg"
                      alt="User Profile Image"
                    />
                  </div>
                </template>
              </div>
            </div> -->
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
  const successMessage = ref(false)

  const { fieldValues, fieldErrors } = storeToRefs(store)
  const formContext = useForm({ initialValues: fieldValues.value })
  store.SetFields(formContext)

  const xmlColumns = {
    name: XmlLoadColumn({ id: 'name', name: 'Name', rules: 'required', visible: true, option: '' }),
    description: XmlLoadColumn({ id: 'description', name: 'Description', rules: 'required', visible: true, option: '' }),
    price: XmlLoadColumn({ id: 'price', name: 'Price', rules: '', visible: true, option: '' }),
  }
  const ClearAll = () => {
    store.ResetStore()
    uploadImageStore.uploadImage = []
  }

  const AddExchange = async () => {
    await store.addExchangeProduct()
    successMessage.value = true
    setTimeout(() => {
      successMessage.value = false
    }, 3000)
  }
</script>
