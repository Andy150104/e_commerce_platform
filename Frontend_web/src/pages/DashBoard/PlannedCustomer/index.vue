<template>

</template>

<style scoped>
  :root {
    --bg-light: #ffffff;
    --bg-dark: #121212;
    --text-light: #000000;
    --text-dark: #ffffff;
    --card-light: #f8f9fa;
    --card-dark: #1e1e1e;
  }
  [data-theme='light'] {
    background-color: var(--bg-light);
    color: var(--text-light);
  }

  [data-theme='dark'] {
    background-color: var(--bg-dark);
    color: var(--text-dark);
  }
  .exchange-list {
    display: flex;
    flex-direction: column;
    gap: 1.5rem;
  }

  .exchange-card {
  display: flex;
  align-items: stretch; /* Đảm bảo các phần tử con có cùng chiều cao */
  justify-content: space-between;
  background-color: var(--card-light);
  color: var(--text-light);
  border: 1px solid #ddd;
  padding: 2rem;
  border-radius: 12px;
  width: 100%;
  max-width: 800px;
  margin: 0 auto;
  gap: 2rem;
  min-height: 180px; /* Không để card bị quá nhỏ */
}
[data-theme="dark"] .exchange-card {
  background-color: var(--card-dark);
  color: var(--text-dark);
  border: 1px solid #444;
}

  .exchange-card:hover {
    transform: scale(1.02);
  }
  .exchange-status {
  font-size: 0.9rem;
  font-weight: bold;
  padding: 0.3rem 0.6rem;
  border-radius: 5px;
  display: inline-block;
  width: fit-content;
  margin-top: 0.5rem; /* Đẩy status xuống dưới */
}

  .status-pending {
    color: #ff9800;
    background-color: #fff3e0;
  }
  .status-changing {
  color: #fdc153; /* Màu cam để thể hiện thay đổi */
  background-color: #fff3e0;
}

  .status-approved {
  background-color: #dff0d8;
  color: #5cb85c;
}

  .status-rejected {
  background-color: #ffdddd;
  color: #d9534f;
}
  .exchange-image-container {
    flex-shrink: 0;
    width: 150px;
    height: 150px;
    overflow: hidden;
    border-radius: 12px;
    cursor: pointer;
  }

  .exchange-image {
    width: 100%;
    height: 100%;
    object-fit: cover;
  }

  .exchange-content {
    flex-grow: 1;
    display: flex;
    justify-content: space-between;
    align-items: center;
    width: 100%;
  }
  .exchange-text {
  flex: 1; /* Để phần chữ mở rộng mà không đẩy nút */
  display: flex;
  flex-direction: column;
  gap: 0.3rem;
}

.exchange-name {
  font-size: 1.2rem;
  font-weight: bold;
  margin: 0;
}

.exchange-description {
  font-size: 0.9rem;
  color: #555;
  word-wrap: break-word;
  overflow-wrap: break-word;
}
/* Fix cho Manage Request luôn căn giữa */
.btn-manage {
  align-self: center;
  flex-shrink: 0; /* Ngăn không cho nút bị bóp lại */
  background-color: #007bff;
  color: white;
  border: none;
  padding: 0.5rem 1rem;
  border-radius: 5px;
  cursor: pointer;
  transition: 0.2s;
}

  .btn-manage:hover {
    background-color: #0056b3;
  }

  /* Modal danh sách ảnh nhỏ */
  .gallery-grid {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(150px, 1fr));
    gap: 10px;
  }

  .gallery-thumbnail {
    cursor: pointer;
    border-radius: 5px;
    overflow: hidden;
  }

  .gallery-thumbnail img {
    width: 100%;
    height: auto;
    border-radius: 5px;
    transition: transform 0.2s ease-in-out;
  }

  .gallery-thumbnail:hover img {
    transform: scale(1.05);
  }

  /* Ảnh lớn trong gallery */
  .gallery-image {
    width: 100%;
    height: auto;
    display: block;
    max-height: 80vh;
  }
  .p-galleria-nav-button {
    pointer-events: auto !important;
    z-index: 10000 !important;
    display: flex !important;
    opacity: 1 !important;
  }

  .p-galleria-prev-button,
  .p-galleria-next-button {
    width: 50px !important;
    height: 50px !important;
    background: rgba(0, 0, 0, 0.5) !important;
    border-radius: 50%;
  }
  .exchange-name {
    color: black !important;
  }
  [data-theme='dark'] .exchange-name {
    color: white !important;
  }
/* 🎨 PANEL CHỨA FILTER */
.filter-panel {
  display: flex;
  gap: 1rem;
  padding: 1rem;
  border-radius: 8px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  background: white;
  max-width: 800px;
  width: 100%;
  margin: 0 auto;
  margin-bottom: 1.5rem;
}
[data-theme='dark'] .filter-panel {
  background: black;
}

/* Input & Select */
.filter-input,
.filter-select {
  flex: 1;
  padding: 0.6rem;
  border: 1px solid #ccc;
  border-radius: 8px;
  font-size: 1rem;
  color: black;
  background: white;
}
.filter-select {
  max-width: 200px;
}
[data-theme='dark'] .filter-input,
[data-theme='dark'] .filter-select {
  color: white;
  background: black;
  border: 1px solid #555;
}
.page-title {
  font-size: 2rem;
  font-weight: bold;
  margin-bottom: 1.5rem;
  text-align: center;
  color: black;
}
[data-theme='dark'] .page-title {
  color: white;
}
.custom-tabmenu .p-tabmenu-nav {
  display: flex;
  justify-content: space-between;
  width: 100%;
}

.custom-tabmenu .p-tabmenu-nav li {
  flex: 1;
  text-align: center;
}

</style>
