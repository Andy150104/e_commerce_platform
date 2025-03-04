export const REQUIRED_ERROR = 'This field is required. Please enter a value.'
export const INPUT_ERROR = 'There is an error in the input.'
export const NUMERIC_ERROR = 'Please enter a numeric value.'
export const MIN_LENGTH_ERROR = 'Please enter at least the required number of characters.'
export const MAX_LENGTH_ERROR = 'Please enter no more than the allowed number of characters.'
export const BETWEEN = 'Please enter a value between {min} and {max} (inclusive).'
export const MIN_MAX = 'Please enter between {min} and {max} characters.'
export const MASTER_CHECK_ERROR = 'The specified code does not exist in the master data. Please specify a different code.'
export const TABLE_CHECK_ERROR = 'The specified code is not registered. Please specify a different code.'
export const MASTER_INPUT_ERROR = 'The specified code already exists. Please specify a different code.'
export const SCD_INPUT_ERROR = 'The product code is already registered. Please enter a unique product code.'
export const MASTER_DELETE_ERROR = 'This record is marked as deleted. Registration cannot be performed.'
export const DATE_ERROR = 'The date is invalid.'
export const DATE_FT_ERROR = 'The date order is incorrect.'
export const DATE_LIMIT_ERROR = 'You cannot enter a date more than one year from today.'
export const DATE_LIMIT_TO_ERROR = 'Please enter the end date within the range {dateFrom} to {maxDate}.'
export const DATE_TODAYBEFORE_ERROR = 'You cannot enter a date before today.'
export const DATE_TODAYBY_ERROR = 'You cannot enter a date that is today or earlier.'
export const DATE_NEXTDAYAFTER_ERROR = 'You cannot enter dates from tomorrow onward.'
export const DATE_TODAYAFTER_ERROR = 'You cannot enter dates from today onward.'
export const LOCK_DATE_ERROR = 'Please enter a date later than the closing date.'
export const IS_ERROR = 'Please make a selection.'
export const KINSOKU_ERROR = 'Forbidden characters have been entered.'
export const NUMBER_FIELD_ERROR = 'Please enter a numeric value.'
export const TIME_FT_ERROR = 'The time order is incorrect.'
export const FILE_IMPORT = 'Select a file'
export const FILE_IMPORT_DEFAULT = 'No file selected'
export const CSVPASS_ERROR = 'The CSV file is invalid. Please select a valid CSV file.'
export const CSVCOUNT_ERROR = 'The number of fields in the CSV file does not match.'
export const CSVFILE_ERROR = 'Please select a CSV file.'
export const EXCELPASS_ERROR = 'The Excel file is invalid. Please select a valid Excel file.'
export const EXCELCOUNT_ERROR = 'The number of fields in the Excel file does not match.'
export const EXCELFILE_ERROR = 'Please select an Excel file.'
export const DATA_EMPTY_ERROR = 'No records found.'
export const PRINT_ERROR = 'There are no reports to print.'
export const SYORI_WARN = 'Extracting data with the selected conditions may take some time.\nDo you want to continue?'
export const DETAIL_NO_CHECK_ERROR = 'No detail data has been selected. Please select at least one record.'
export const SELECTSYUKKO_INSERT = 'Registration completed.'
export const ORDERSANSYO_INSERT = 'Registration completed.'
export const CODE_GARA_DUPLICATE = 'Registration cannot be completed because the associated garage is the same as the previous record.'
export const CODE_CARD_DUPLICATE = 'Registration cannot be completed because the card number is identical to the previous card number.'
export const DATE_RANGE_ERROR = 'Registration cannot be completed due to a gap within the specified period.'
export const DATE_NOT_DUPLICATE = 'Registration cannot be completed with the provided association date.'
export const NUMBER_FT = 'Please enter a number between {min} and {max}.'
export const MAIL_ERROR = 'Invalid format. Please enter a valid email address.'
export const KATAKANA_ERROR = 'Please use full-width Katakana characters.'
export const MAX_LINE_ERROR = 'Please enter no more than {max} lines.'
export const MAX_LINE_CHAR_ERROR = 'Please enter no more than {max} characters per line.'
export const PRICE_FT_ERROR = 'The price limits are incorrect.'
export const NUMBER_FT_ERROR = 'The numeric order is incorrect.'
export const NUMERIC_FT_ERROR = 'The numeric order is incorrect.'
export const FETCH_SCALE_ERROR = 'Failed to retrieve the scale digits for the amount. This amount input cannot be used.'
export const DENWA_BANGOU_INPUT_ERROR = 'Please enter all daytime contact numbers.'
export const DENWA_BANGOU_DIGITS_ERROR = 'Please enter the phone number as 10 or 11 digits.'
export const DENWA_BANGOU_DATETIME_DIGITS_ERROR = 'Please enter the daytime contact number with 10 to 11 digits.'
export const INPUT_TWIN_ERROR = 'Please fill in both fields.'
export const FIXED_LENGTH_ERROR = 'Please enter exactly {fixedLength} characters.'
export const CONFIRM_EMAIL = 'Please enter the same value for the email address (confirmation).'
export const HALF_STRING_ERROR = 'Please use half-width characters.'
export const CONFIRM_PASSWORD = 'Please enter the same value for the password (confirmation).'
export const CONFIRM_NEW_PASSWORD_ERROR = 'Password does not match'
export const PASSWORD_SYMBOL_ERROR =
  'The password contains invalid characters.\nOnly half-width alphanumeric characters and the symbols !@#$%&*+:?;/. are allowed.'
export const PASSWORD_COMBINE_ERROR = 'The password must combine at least two of the following: uppercase letters, lowercase letters, and digits.'
export const PASSWORD_START_WITH_EMAIL_ERROR = 'Please choose a password that does not start with your email address.'
export const HALF_STRING_NUMBER_ERROR = 'Please enter using half-width alphanumeric characters.'

export const DELETE_WARN = {
  messageId: 'W999999',
  message: 'This record is marked as deleted. Do you want to proceed with the correction process?',
}
export const AUTH_ERROR = {
  messageId: 'E999991',
  message: 'The customer number, email address, or password is incorrect.',
}
export const AUTH_LOCK = {
  messageId: 'E999992',
  message: 'Your account has been locked.\nPlease try logging in after some time.',
}
export const TIMEOUT_ERROR = {
  messageId: 'E999993',
  message: 'Login session timed out. Please log in again.',
}
export const SYSTEM_ERROR = {
  messageId: 'E999999',
  message: 'A system error has occurred.',
}
export const SYSTEM_ERROR_BACKEND = {
  messageId: 'E999999',
  message: 'A system error has occurred (backend).',
}
export const SYSTEM_ERROR_IDENTITY = {
  messageId: 'E999999',
  message: 'A system error has occurred (identity).',
}
export const MESSAGE_OK = {
  messageId: 'I00000',
  message: 'The process completed successfully.',
}
