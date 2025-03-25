<template>
  <BaseScreenProduct>
    <template #body>
      <div class="pb-8 bg-white md:pb-16 dark:bg-gray-900 antialiased">
        <div class="max-w-screen-2xl px-4 mx-auto 2xl:px-0">
          <!-- Header Section with bottom divider -->
          <div class="border-b-2 border-gray-400 dark:border-gray-800 pb-4 mb-4">
            <h1 class="text-3xl font-semibold text-gray-900 sm:text-2xl dark:text-white mt-3 mb-4">Exchange Details</h1>
            <BreadCrumb
              :breadcrumbs="[
                { label: 'Service', link: '#' },
                { label: 'Exchange', link: '/Service/Exchange' },
                { label: 'Blindbox', link: '#' },
              ]"
            />
          </div>

          <!-- Product Name -->
          <h1 class="text-xl font-semibold text-gray-900 sm:text-2xl dark:text-white my-6">
            {{ ExchangeStore.exchangeDetails?.exchangeName }}
          </h1>

          <!-- Main Grid -->
          <div class="lg:grid lg:grid-cols-3 lg:gap-8 xl:gap-16 mt-4">
            <!-- Gallery Carousel with a right divider on large screens -->
            <div class="shrink-0 max-w-full lg:col-span-3 mx-auto lg:pr-8 border-gray-400 dark:border-gray-800">
              <GalleryCarousel :images="imageGalleryList" />
              <!-- Product Info -->
            </div>
            <div class="mt-6 sm:mt-8 col-span-3 lg:mt-0 bg-gray-100 rounded-md p-6 border-spacing-2 border-b-0 break-words">
              <div class="mt-4 sm:items-center sm:gap-4 sm:flex">
                <p class="text-2xl font-extrabold text-gray-900 sm:text-3xl dark:text-white">{{ store.productDetail.price }}</p>
              </div>
              <div class="mt-6 sm:gap-4 sm:items-center sm:flex sm:mt-8">
                <ModalInputForm
                  :header="'Exchange Info'"
                  :button-icon="'pi pi-plus'"
                  :button-label="'Exchange Now'"
                  :button-severity="'contrast'"
                  :width="'80rem'"
                  @on-confirm="onConfirm"
                  @on-blinding="onBinding"
                >
                  <template #body>
                    <BaseControlEditorInput :xml-column="xmlColumns.description" v-model="editorValue" />
                  </template>
                </ModalInputForm>
              </div>
              <hr class="my-6 md:my-8 border-gray-400 dark:border-gray-800" />
            </div>

            <!-- Accordion with top divider -->
            <div class="md:col-span-3 mt-8 border-t-2 border-gray-400 dark:border-gray-800 pt-8">
              <div
                v-html="ExchangeStore.exchangeDetails?.description"
                class="mb-6 text-gray-500 dark:text-gray-400 whitespace-normal break-words overflow-auto"
              ></div>
              <Accordion />
            </div>
          </div>
        </div>
      </div>
    </template>
  </BaseScreenProduct>
</template>

<script setup lang="ts">
  import Accordion from '@PKG_SRC/components/Accordion/Accordion.vue'
  import BaseControlEditorInput from '@PKG_SRC/components/Basecontrol/BaseControlEditorInput.vue'
  import BreadCrumb from '@PKG_SRC/components/BreadCrumb/BreadCrumb.vue'
  import GalleryCarousel from '@PKG_SRC/components/Gallery/GalleryCarousel.vue'
  import ModalInputForm from '@PKG_SRC/components/Modal/ModalInputForm.vue'
  import BaseScreenProduct from '@PKG_SRC/layouts/Basecreen/BaseScreenProduct.vue'
  import { useCartStore } from '@PKG_SRC/stores/Modules/Blind_Box/CartStore'
  import { useDetailProductStore } from '@PKG_SRC/stores/Modules/Blind_Box/DetailProductStore'
  import { useRequestStore } from '@PKG_SRC/stores/Modules/Blind_Box/ExchangeRequestStore'
  import { useExchangeStore } from '@PKG_SRC/stores/Modules/Blind_Box/ExchangeStore'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import { Drawer } from 'flowbite'
  import { useForm } from 'vee-validate'

  const editorValue = ref('')
  const store = useDetailProductStore()
  const requestStore = useRequestStore()
  const cartStore = useCartStore()
  const ExchangeStore = useExchangeStore()
  const isCartVisible = ref(true)
  const isUpdated = ref(false)
  const route = useRoute()
  const { fieldValues, fieldErrors } = storeToRefs(requestStore)
  const formContext = useForm({ initialValues: fieldValues.value })
  requestStore.SetFields(formContext)
  const xmlColumns = {
    quantity: XmlLoadColumn({
      id: 'quantity',
      name: 'quantity',
      rules: '',
      visible: true,
      option: '',
    }),
    description: XmlLoadColumn({
      id: 'description',
      name: 'Description',
      rules: '',
      visible: true,
      option: '',
    }),
    exchangeId: XmlLoadColumn({
      id: 'exchangeId',
      name: 'ExchangeId',
      rules: '',
      visible: true,
      option: '',
    }),
  }
  const onBinding = async (items: any) => {
    await nextTick()
    requestStore.fields.setFieldValue('description', items.description)
    editorValue.value = items.description
  }

  const codeProduct = computed(() => {
    const id = route.params.id
    return Array.isArray(id) ? id[0] : id
  })
  const imageGalleryList = computed(() => {
    return store.convertImageList(ExchangeStore.imageList?.map((imageUrl) => ({ imageUrl })) ?? [])
  })

  const onConfirm = async () => {
    formContext.setFieldValue('ExchangeId', codeProduct.value)
    formContext.setFieldValue('description', editorValue.value)
    await nextTick()
    await requestStore.AddToQueue()
    requestStore.ResetStore()
    editorValue.value = ''
  }

  onMounted(async () => {
    if (!(await ExchangeStore.GetByExchangeID(codeProduct.value))) {
      console.log(store.convertImageList(store.productDetail.imageUrls || []))
    }
  })
</script>
