import { defineRule, configure } from 'vee-validate'
import { localize, setLocale } from '@vee-validate/i18n'
import { setLocale as setYupLocale } from 'yup'
import { suggestive } from 'yup-locale-ja'
import {
  DATE_LIMIT_ERROR,
  DATE_LIMIT_TO_ERROR,
  DATE_NEXTDAYAFTER_ERROR,
  DATE_TODAYAFTER_ERROR,
  DATE_TODAYBEFORE_ERROR,
  DATE_TODAYBY_ERROR,
  KINSOKU_ERROR,
  NUMBER_FIELD_ERROR,
  NUMERIC_ERROR,
  TIME_FT_ERROR,
  REQUIRED_ERROR,
  MIN_LENGTH_ERROR,
  MAX_LENGTH_ERROR,
  BETWEEN,
  MIN_MAX,
  DATE_FT_ERROR,
  IS_ERROR,
  DATE_ERROR,
  LOCK_DATE_ERROR,
  NUMBER_FT,
  MAIL_ERROR,
  KATAKANA_ERROR,
  MAX_LINE_ERROR,
  MAX_LINE_CHAR_ERROR,
  NUMERIC_FT_ERROR,
  PRICE_FT_ERROR,
  NUMBER_FT_ERROR,
  DENWA_BANGOU_INPUT_ERROR,
  DENWA_BANGOU_DIGITS_ERROR,
  DENWA_BANGOU_DATETIME_DIGITS_ERROR,
  INPUT_TWIN_ERROR,
  HALF_STRING_ERROR,
  FIXED_LENGTH_ERROR,
  CONFIRM_EMAIL,
  CONFIRM_PASSWORD,
  CONFIRM_NEW_PASSWORD_ERROR,
  PASSWORD_SYMBOL_ERROR,
  PASSWORD_COMBINE_ERROR,
  PASSWORD_START_WITH_EMAIL_ERROR,
  HALF_STRING_NUMBER_ERROR,
} from '@PKG_SRC/utils/message'
import { defineNuxtPlugin } from 'nuxt/app'
import moment from 'moment'
import { DateFormat } from '@PKG_SRC/types/enums/constantFrontend'
setYupLocale(suggestive)

/**
 * バリデートを使う/使わない
 */
export const useValidate = (type: string | undefined) => {
  // 入力中はバリデートを使わない
  if (type === 'input') return false
  return true
}
export default defineNuxtPlugin(() => {
  configure({
    generateMessage: localize('en', {
      messages: {
        required: REQUIRED_ERROR,
        email: CONFIRM_EMAIL,
        min: MIN_LENGTH_ERROR,
        min_max: MIN_MAX,
      },
    }),
    validateOnBlur: true,
    validateOnChange: false,
    validateOnInput: false,
    validateOnModelUpdate: false,
  })
  // Định nghĩa quy tắc mặc định
  defineRule('required', (value: string) => {
    return value ? true : 'This field is required.'
  })

  defineRule('email', (value: string) => {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
    return emailRegex.test(value) ? true : 'Please enter a valid email address.'
  })
  defineRule('date_format', (value: string, [target]: string) => {
    const input = document.getElementById(target as string) as HTMLInputElement
    if (input == null || !useValidate(window.event?.type)) {
      return true
    }
    if (input.validity.badInput) {
      return false
    }
    if (moment(convertDateFormat(value, DateFormat.YYYYMMDD)).isBefore('1000-01-01')) {
      return false
    }
    if (moment(convertDateFormat(value, DateFormat.YYYYMMDD)).isAfter('2999-12-31')) {
      return false
    }
    return true
  })
  defineRule('confirm_password', (value: string, [target]: string) => {
    if (!value || !useValidate(window.event?.type)) return true
    const password = (document.getElementsByName(target)[0] as HTMLInputElement)?.value ?? ''
    return value === password || CONFIRM_NEW_PASSWORD_ERROR
  })
  defineRule('min_max', (value: string, [min, max]: string) => {
    if (
      value === '' ||
      value === undefined ||
      value === null ||
      !useValidate(window.event?.type)
    ) {
      return true
    }
    const numericValue = Number(value)
    if (isNaN(numericValue)) {
      return NUMERIC_ERROR
    }
    if (numericValue >= Number(min) && numericValue <= Number(max)) {
      return true
    }
    return MIN_MAX.replace('{min}', min).replace('{max}', max)
  })
  
  setLocale('en')
})
