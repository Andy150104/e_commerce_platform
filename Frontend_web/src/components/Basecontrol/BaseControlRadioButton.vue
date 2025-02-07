<template>
  <div class="flex">
    <div v-for="option in options" :key="option.value" class="flex items-center me-4">
      <Field
        class="w-6 h-6 text-blue-600 bg-gray-100 border-gray-300 focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600"
        :id="fieldId + option.value"
        :name="xmlColumn.id"
        type="radio"
        :value="option.value"
        :checked="option.checked"
        :disabled="disabled"
      />
      <label :value="option.value" class="ms-2 text-sm font-medium text-gray-900 dark:text-gray-300"> {{ option.label }}</label>
    </div>
  </div>
</template>
<script setup lang="ts">
  import { uuid } from 'vue-uuid'
  import { Field } from 'vee-validate'
  import type { xmlColumn } from '@PKG_SRC/utils/xml'


  const props = defineProps({
    model: {
      type: [String, Number],
      required: true,
    },
    xmlColumn: {
      type: Object as PropType<xmlColumn>,
      required: true,
    },
    masterName: {
      type: String as () => MasterName,
      required: true,
    },
    errMsg: {
      type: String,
      default: '',
    },
    params: {
      type: Object,
      required: false,
      default: () => ({}),
    },
    disabled: {
      type: Boolean,
      required: true,
    },
    move: {
      type: Boolean,
      required: false,
      default: true,
    },
    formClass: {
      type: String,
      required: false,
      default: '',
    },
    isShowError: {
      type: Boolean,
      required: false,
      default: true,
    },
  })

  const options = ref<any>([])
  const fieldId = uuid.v4()
  const fieldRules = ref(props.xmlColumn.rules)

  const fetch = async () => {
    await nextTick()
    options.value = await getSelectComponentData(props.masterName, {
      xmlColumn: props.xmlColumn,
      ...(props.params as any),
    })
  }

  const onFocus = () => {
    const id = document.getElementById(fieldId) as HTMLInputElement
    id.focus()
  }

  onMounted(async () => {
    watch(props, (value) => {
      fieldRules.value = value?.xmlColumn?.rules
    })
    await nextTick()
    await fetch()
  })

  defineExpose({
    fetch,
    onFocus,
  })
</script>
