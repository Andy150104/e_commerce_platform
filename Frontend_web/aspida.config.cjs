// module.exports = {
//   input: 'define', // Đường dẫn file OpenAPI
//   baseURL: '',
//   outputEachDir: false,         // Nếu true, tạo thư mục riêng cho từng file API
//   openapi: { inputFile: 'openapi-auth.json' },
// };
module.exports = {
  input: 'api', // Đường dẫn file OpenAPI
  baseURL: '',
  outputEachDir: false, // Nếu true, tạo thư mục riêng cho từng file API
  openapi: { inputFile: 'openapi.json' },
}
