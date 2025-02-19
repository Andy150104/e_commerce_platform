export const REQUIRED_ERROR = '入力必須項目です。入力してください。'
export const INPUT_ERROR = '入力内容に誤りがあります。'
export const NUMERIC_ERROR = '数字で入力してください。'
export const MIN_LENGTH_ERROR = '文字以上で入力してください。'
export const MAX_LENGTH_ERROR = '文字以内で入力してください。'
export const BETWEEN = '0:{min}以上1:{max}以下で入力してください。'
export const MIN_MAX = '0:{min}文字以上から1:{max}文字以内で入力してください。'
export const MASTER_CHECK_ERROR = '指定のコードはマスタに存在しません。別のコードを指定してください。'
export const TABLE_CHECK_ERROR = '指定のコードは登録されていません。別のコードを指定してください。'
export const MASTER_INPUT_ERROR = '指定のコードは既に存在します。別のコードを指定してください。'
export const SCD_INPUT_ERROR = '商品コードは既に登録されています。未登録のデータを入力してください。'
export const MASTER_DELETE_ERROR = '削除データです。登録処理は行えません。'
export const DATE_ERROR = '日付が不正です。'
export const DATE_FT_ERROR = '日付の大小関係が誤っています。'
export const DATE_LIMIT_ERROR = '当日より1年先以降の日付は入力できません。'
export const DATE_LIMIT_TO_ERROR = '日付（終了）は 0:{dateFrom} ～ 1:{maxDate} の範囲で入力して下さい。'
export const DATE_TODAYBEFORE_ERROR = '本日より前の日付は入力できません。'
export const DATE_TODAYBY_ERROR = '本日以前の日付は入力できません。'
export const DATE_NEXTDAYAFTER_ERROR = '翌日以降の日付は入力できません。'
export const DATE_TODAYAFTER_ERROR = '本日以降の日付は入力できません。'
export const LOCK_DATE_ERROR = '締日より後の日付を入力してください。'
export const IS_ERROR = '選択してください。'
export const KINSOKU_ERROR = '禁則文字が入力されています。'
export const NUMBER_FIELD_ERROR = '数値を入力してください。'
export const TIME_FT_ERROR = '時刻の大小関係が誤っています。'
export const FILE_IMPORT = 'ファイルを選択'
export const FILE_IMPORT_DEFAULT = '選択されていません'
export const CSVPASS_ERROR = 'CSVファイルが正しくありません。正しいCSVファイルを選択してください。'
export const CSVCOUNT_ERROR = 'CSVファイルの項目数が一致しません。'
export const CSVFILE_ERROR = 'CSVファイルを選択してください。'
export const EXCELPASS_ERROR = 'EXCELファイルが正しくありません。正しいEXCELファイルを選択してください。'
export const EXCELCOUNT_ERROR = 'EXCELファイルの項目数が一致しません。'
export const EXCELFILE_ERROR = 'EXCELファイルを選択してください。'
export const DATA_EMPTY_ERROR = 'データは0件です'
export const PRINT_ERROR = '出力する帳票はありません。'
export const SYORI_WARN = '選択された条件で抽出する場合、時間がかかる場合があります。\n処理を継続してよろしいですか？'
export const DETAIL_NO_CHECK_ERROR = '明細データが選択されていません。1件以上選択してください。'
export const SELECTSYUKKO_INSERT = '登録を完了しました。'
export const ORDERSANSYO_INSERT = '登録を完了しました。'
export const CODE_GARA_DUPLICATE = '所属車庫が前の履歴車庫と同じなため、 登録ができません。'
export const CODE_CARD_DUPLICATE = 'カードNoが前のカードNoと同じなため、登録ができません。'
export const DATE_RANGE_ERROR = '所属期間内に空白期間があるため、登録できません。'
export const DATE_NOT_DUPLICATE = '入力した所属年月日では登録できません。'
export const NUMBER_FT = '0:{min}以上1:{max}以下で入力してください。'
export const MAIL_ERROR = '有効なフォーマットではありません。正しく入力してください。'
export const KATAKANA_ERROR = '全角カタカナで入力してください。'
export const MAX_LINE_ERROR = '0:{max}行以内で入力してください。'
export const MAX_LINE_CHAR_ERROR = '1行0:{max}文字以内で入力してください。'
export const PRICE_FT_ERROR = '金額の大小関係が誤っています。'
export const NUMBER_FT_ERROR = '数値の大小関係が誤っています。'
export const NUMERIC_FT_ERROR = '数字の大小関係が誤っています。'
export const FETCH_SCALE_ERROR = '金額入力のスケール桁数の取得に失敗しました。この金額入力は使用できません。'
export const DENWA_BANGOU_INPUT_ERROR = '日中連絡先を全て入力してください。'
export const DENWA_BANGOU_DIGITS_ERROR = '電話番号は10桁または11桁で入力してください。'
export const DENWA_BANGOU_DATETIME_DIGITS_ERROR = '日中連絡先は10桁以上11桁以下で入力してください。'
export const INPUT_TWIN_ERROR = '両方入力してください。'
export const FIXED_LENGTH_ERROR = '0:{fixedLength}文字で入力してください。'
export const CONFIRM_EMAIL = 'メールアドレス(確認)にはメールアドレスと同じ値を入力して下さい。'
export const HALF_STRING_ERROR = '半角で入力してください。'
export const CONFIRM_PASSWORD = 'パスワード(確認)にはパスワードと同じ値を入力して下さい。'
export const CONFIRM_NEW_PASSWORD_ERROR = 'Password does not match'
export const PASSWORD_SYMBOL_ERROR =
  'パスワードに使用できない文字が含まれています。\nパスワードは半角英数字と半角記号!@#$%&*+:?;/.のみを使用してください。'
export const PASSWORD_COMBINE_ERROR = '	パスワードには半角英字(大文字)、半角英字(小文字)、半角数字のうち2つ以上を組み合わせてください。'
export const PASSWORD_START_WITH_EMAIL_ERROR = 'パスワードにはメールアドレスの先頭部分と異なる値を入力して下さい。'
export const HALF_STRING_NUMBER_ERROR = '半角英数で入力してください。'

export const DELETE_WARN = {
  messageId: 'W999999',
  message: '削除データになっています。修正処理を行いますか？',
}
export const AUTH_ERROR = {
  messageId: 'E999991',
  message: 'お客様番号またはメールアドレス、パスワードが間違っています。',
}
export const AUTH_LOCK = {
  messageId: 'E999992',
  message: 'アカウントをロックしました。\n一定時間経過した後にログインしてください。',
}
export const TIMEOUT_ERROR = {
  messageId: 'E999993',
  message: 'ログイン認証がタイムアウトしました。 再度ログインしてください。',
}
export const SYSTEM_ERROR = {
  messageId: 'E999999',
  message: 'システムエラーが発生しました。',
}
export const SYSTEM_ERROR_BACKEND = {
  messageId: 'E999999',
  message: 'システムエラーが発生しました。(backend)',
}
export const SYSTEM_ERROR_IDENTITY = {
  messageId: 'E999999',
  message: 'システムエラーが発生しました。(identity)',
}
export const MESSAGE_OK = {
  messageId: 'I00000',
  message: '処理が正常に終了しました。',
}
