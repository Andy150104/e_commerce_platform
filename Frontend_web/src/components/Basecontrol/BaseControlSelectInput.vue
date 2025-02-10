<template>
  <div>
    <select
      id="countries"
      class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
    >
      <option selected>Choose a country</option>
      <option value="US">United States</option>
      <option value="CA">Canada</option>
      <option value="FR">France</option>
      <option value="DE">Germany</option>
    </select>
    <Field
      :id="fieldId"
      :class="className.BUTTON_DEFAULT_BLUE_1"
      :name="xmlColumn.id"
      as="select"
      :rules="fieldRules"
      @blur="onBlur"
      @change="onChange"
    >
    </Field>
  </div>
</template>
<script setup lang="ts">
  import { className } from '@PKG_SRC/utils/class/className'
  import type { xmlColumn } from '@PKG_SRC/utils/xml'
  import { Field } from 'vee-validate'
  import { uuid } from 'vue-uuid'

  const props = defineProps({
    model: {
      type: [String, Number],
      required: true,
    },
    xmlColumn: {
      type: Object as PropType<xmlColumn>,
      required: true,
    },
    disabled: {
      type: Boolean,
      required: true,
    },
    params: {
      type: Object,
      required: false,
      default: () => ({}),
    },
    masterName: {
      type: String as () => MasterName,
      required: true,
    },
    errMsg: {
      type: String,
      default: '',
    },
    isDisable: {
      type: Boolean,
      required: false,
      default: false,
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

  interface Emits {
    (e: 'on-blur'): void
    (e: 'on-change'): void
  }

  const emit = defineEmits<Emits>()

  const onBlur = () => {
    emit('on-blur')
  }

  const onChange = () => {
    emit('on-change')
  }

  const fetch = async (params = {}) => {
    await nextTick()
    options.value = await getSelectComponentData(props.masterName, {
      xmlColumn: props.xmlColumn,
      ...props.params,
      ...params,
    })
  }
</script>
