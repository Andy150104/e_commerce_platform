export function formatMoney(money: number, currency: string, locale: string = 'en-US') {
  const moneyString = money.toString();
  const fractionDigits = moneyString.includes('.') ? moneyString.split('.')[1].length : 0;

  const formatter = new Intl.NumberFormat(locale, {
    style: 'currency',
    currency: currency,
    currencyDisplay: 'code',
    minimumFractionDigits: fractionDigits,
    maximumFractionDigits: fractionDigits,
  });

  return formatter.format(money);
}

