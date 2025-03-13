import { DateFormat } from '@PKG_SRC/types/enums/constantFrontend'
import moment from 'moment'

export function convertDateFormat(inputDate: string, toFormat: DateFormat) {
  const possibleFormats = Object.values(DateFormat)
  const detectedFormat = possibleFormats.find((format) => moment(inputDate, format, true).isValid())
  return moment(inputDate, detectedFormat).format(toFormat)
}
