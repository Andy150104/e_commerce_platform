export function formatMoney(money: number, currency: string, locale: string = 'en-US') {
  const formatter = new Intl.NumberFormat(locale, {
    style: 'currency',
    currency: currency,
    currencyDisplay: 'code',
  })

  return formatter.format(money)
}
