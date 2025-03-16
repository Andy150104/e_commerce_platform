<template>
    <div class="bg-gray-100">
  <div class="container mx-auto px-4 py-8">
    <!-- Chia layout thành 2 cột: 1 cột (left) hiển thị danh sách job, 1 cột (right) hiển thị chi tiết -->
    <div class="grid grid-cols-1 gap-6 md:grid-cols-[1fr_2fr]">
      <!-- CỘT TRÁI: Danh sách thẻ (cards) - KHÔNG dùng overflow-y-auto -->
      <div class="h-full space-y-4 overflow-y-auto">
        <!-- Card 1 -->
        <div class="relative rounded-lg border border-gray-200 bg-white p-4 shadow">
          <span class="absolute top-4 right-4 rounded bg-red-500 px-2 py-1 text-xs font-semibold text-white"> HOT </span>
          <h2 class="mb-2 text-lg font-semibold">Web Dev Fullstack (React, .NET, Python)</h2>
          <div class="mb-3 flex items-center space-x-2">
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">Hybrid</span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">.NET</span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">Product Manager</span>
          </div>
          <p class="mb-4 text-gray-600">Tham gia phát triển fullstack với React, .NET, Python, phối hợp cùng team...</p>
          <button class="rounded bg-blue-500 px-4 py-2 font-medium text-white hover:bg-blue-600">Xem chi tiết</button>
        </div>

        <!-- Card 2 -->
        <div class="relative rounded-lg border border-gray-200 bg-white p-4 shadow">
          <span class="absolute top-4 right-4 rounded bg-orange-500 px-2 py-1 text-xs font-semibold text-white"> SUPER </span>
          <h2 class="mb-2 text-lg font-semibold">Senior Full Stack Engineer (.NET, JavaScript, English)</h2>
          <div class="mb-3 flex items-center space-x-2">
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">Hybrid</span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">.NET</span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">JavaScript</span>
          </div>
          <p class="mb-4 text-gray-600">Làm việc với các công nghệ .NET, JavaScript, yêu cầu tiếng Anh tốt...</p>
          <button class="rounded bg-blue-500 px-4 py-2 font-medium text-white hover:bg-blue-600">Xem chi tiết</button>
        </div>

        <!-- Card 3 -->
        <div class="relative rounded-lg border border-gray-200 bg-white p-4 shadow">
          <span class="absolute top-4 right-4 rounded bg-green-500 px-2 py-1 text-xs font-semibold text-white"> NEW </span>
          <h2 class="mb-2 text-lg font-semibold">Junior .NET Developer ~ up to $2000</h2>
          <div class="mb-3 flex items-center space-x-2">
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">On-site</span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">.NET</span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">C#</span>
          </div>
          <p class="mb-4 text-gray-600">Tham gia vào dự án .NET, được đào tạo nâng cao kỹ năng, cơ hội thăng tiến...</p>
          <button class="rounded bg-blue-500 px-4 py-2 font-medium text-white hover:bg-blue-600">Xem chi tiết</button>
        </div>

        <!-- Card 4 -->
        <div class="relative rounded-lg border border-gray-200 bg-white p-4 shadow">
          <h2 class="mb-2 text-lg font-semibold">Lead Software Engineer (DevOps)</h2>
          <div class="mb-3 flex items-center space-x-2">
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">Remote</span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">AWS</span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">Docker</span>
          </div>
          <p class="mb-4 text-gray-600">Xây dựng hệ thống CI/CD, quản lý hạ tầng AWS, đảm bảo tính sẵn sàng...</p>
          <button class="rounded bg-blue-500 px-4 py-2 font-medium text-white hover:bg-blue-600">Xem chi tiết</button>
        </div>

        <!-- Card 5 -->
        <div class="relative rounded-lg border border-gray-200 bg-white p-4 shadow">
          <span class="absolute top-4 right-4 rounded bg-purple-500 px-2 py-1 text-xs font-semibold text-white"> TREND </span>
          <h2 class="mb-2 text-lg font-semibold">Frontend Developer (React, Next.js)</h2>
          <div class="mb-3 flex items-center space-x-2">
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">Remote</span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">React</span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">Next.js</span>
          </div>
          <p class="mb-4 text-gray-600">Phát triển giao diện người dùng bằng React, Next.js, tối ưu SEO...</p>
          <button class="rounded bg-blue-500 px-4 py-2 font-medium text-white hover:bg-blue-600">Xem chi tiết</button>
        </div>

        <!-- Card 6 -->
        <div class="relative rounded-lg border border-gray-200 bg-white p-4 shadow">
          <h2 class="mb-2 text-lg font-semibold">Backend Engineer (Node.js, MongoDB)</h2>
          <div class="mb-3 flex items-center space-x-2">
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">On-site</span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">Node.js</span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">MongoDB</span>
          </div>
          <p class="mb-4 text-gray-600">Thiết kế, xây dựng API, tối ưu hiệu năng cho hệ thống backend...</p>
          <button class="rounded bg-blue-500 px-4 py-2 font-medium text-white hover:bg-blue-600">Xem chi tiết</button>
        </div>

        <!-- Card 7 -->
        <div class="relative rounded-lg border border-gray-200 bg-white p-4 shadow">
          <span class="absolute top-4 right-4 rounded bg-blue-600 px-2 py-1 text-xs font-semibold text-white"> TOP </span>
          <h2 class="mb-2 text-lg font-semibold">Mobile App Developer (iOS/Android)</h2>
          <div class="mb-3 flex items-center space-x-2">
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">Hybrid</span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">iOS</span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">Android</span>
          </div>
          <p class="mb-4 text-gray-600">Tham gia phát triển ứng dụng di động trên iOS và Android, dùng React Native hoặc Swift/Kotlin...</p>
          <button class="rounded bg-blue-500 px-4 py-2 font-medium text-white hover:bg-blue-600">Xem chi tiết</button>
        </div>

        <!-- Card 8 -->
        <div class="relative rounded-lg border border-gray-200 bg-white p-4 shadow">
          <h2 class="mb-2 text-lg font-semibold">QA/QC Engineer</h2>
          <div class="mb-3 flex items-center space-x-2">
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">Full-time</span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">Testing</span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">Automation</span>
          </div>
          <p class="mb-4 text-gray-600">Xây dựng kịch bản test, đảm bảo chất lượng sản phẩm, phối hợp với Dev & PM...</p>
          <button class="rounded bg-blue-500 px-4 py-2 font-medium text-white hover:bg-blue-600">Xem chi tiết</button>
        </div>

        <!-- Card 9 -->
        <div class="relative rounded-lg border border-gray-200 bg-white p-4 shadow">
          <span class="absolute top-4 right-4 rounded bg-yellow-500 px-2 py-1 text-xs font-semibold text-white"> HOT </span>
          <h2 class="mb-2 text-lg font-semibold">Data Scientist (Python, Machine Learning)</h2>
          <div class="mb-3 flex items-center space-x-2">
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">Remote</span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">Python</span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">ML</span>
          </div>
          <p class="mb-4 text-gray-600">Phân tích dữ liệu, xây dựng mô hình máy học, dự đoán xu hướng kinh doanh...</p>
          <button class="rounded bg-blue-500 px-4 py-2 font-medium text-white hover:bg-blue-600">Xem chi tiết</button>
        </div>

        <!-- Card 10 -->
        <div class="relative rounded-lg border border-gray-200 bg-white p-4 shadow">
          <h2 class="mb-2 text-lg font-semibold">Product Owner / Business Analyst</h2>
          <div class="mb-3 flex items-center space-x-2">
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">Full-time</span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">Scrum</span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800">Agile</span>
          </div>
          <p class="mb-4 text-gray-600">Thu thập yêu cầu, phân tích nghiệp vụ, làm việc với đội ngũ phát triển...</p>
          <button class="rounded bg-blue-500 px-4 py-2 font-medium text-white hover:bg-blue-600">Xem chi tiết</button>
        </div>
      </div>

      <!-- CỘT PHẢI: Sticky để không trôi khi trang cuộn -->
      <div class="sticky top-0 h-fit rounded-lg border border-gray-200 bg-white p-6 shadow">
        <!-- Tiêu đề, nút Apply -->
        <div class="mb-6 flex items-start justify-between">
          <div>
            <h2 class="mb-2 text-2xl font-semibold">Web Dev Fullstack (React, .NET, Python)</h2>
            <p class="text-gray-600">Công ty HICAS</p>
          </div>
          <button class="rounded bg-red-500 px-4 py-2 font-medium text-white hover:bg-red-600">Apply now</button>
        </div>

        <!-- Thông tin tổng quan -->
        <div class="mb-6">
          <p class="mb-2 text-gray-600"><strong>Địa chỉ:</strong> Tầng 3, 45 Võ Văn Hàm, Quận X, TP. Hồ Chí Minh</p>
          <p class="mb-2 text-gray-600"><strong>Hình thức:</strong> Full-time</p>
          <p class="mb-2 text-gray-600"><strong>Mức lương:</strong> Thỏa thuận</p>
          <div class="flex items-center space-x-2">
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800"> React </span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800"> .NET </span>
            <span class="inline-block rounded bg-gray-100 px-2 py-1 text-sm text-gray-800"> Python </span>
          </div>
        </div>

        <!-- Top 3 reasons to join us -->
        <div class="mb-6">
          <h3 class="mb-2 text-xl font-semibold">Top 3 reasons to join us</h3>
          <ul class="list-inside list-disc space-y-1 text-gray-600">
            <li>Mức đãi ngộ cạnh tranh (lương 13, thưởng quý).</li>
            <li>Môi trường chuyên nghiệp, năng động, đồng nghiệp thân thiện.</li>
            <li>Cơ hội phát triển và thăng tiến nhanh, tham gia các dự án lớn.</li>
          </ul>
        </div>

        <!-- Job description -->
        <div class="mb-6">
          <h3 class="mb-2 text-xl font-semibold">Job description</h3>
          <p class="mb-4 text-gray-600">- Phát triển ứng dụng web với React (front-end) và .NET (back-end). - Kết nối các dịch vụ nội bộ bằng Python khi cần tích hợp, xử lý dữ liệu. - Tối ưu code, tối ưu hiệu năng, làm việc chặt chẽ với team QA, PM...</p>
          <p class="mb-4 text-gray-600">- Tích hợp với các API của bên thứ ba khi cần. - Đảm bảo tiến độ dự án, báo cáo công việc theo yêu cầu. - Tham gia review code, chia sẻ kinh nghiệm với đồng đội.</p>
          <p class="text-gray-600">- Thực hiện các nhiệm vụ khác do cấp trên giao phó.</p>
        </div>

        <!-- Yêu cầu công việc -->
        <div class="mb-6">
          <h3 class="mb-2 text-xl font-semibold">Yêu cầu công việc</h3>
          <p class="mb-4 text-gray-600">- Có kinh nghiệm làm việc với React, .NET (tối thiểu 1-2 năm). - Hiểu về quy trình phát triển phần mềm, CI/CD, Git. - Kỹ năng giao tiếp tốt, tư duy giải quyết vấn đề.</p>
          <p class="mb-4 text-gray-600">- Ưu tiên ứng viên có kinh nghiệm với Docker, AWS, hoặc Azure. - Biết Python là lợi thế lớn. - Tiếng Anh đọc hiểu tài liệu, giao tiếp cơ bản là điểm cộng.</p>
        </div>

        <!-- Kỹ năng và kinh nghiệm -->
        <div class="mb-6">
          <h3 class="mb-2 text-xl font-semibold">Your skills and experiences</h3>
          <ul class="list-inside list-disc space-y-1 text-gray-600">
            <li>Tốt nghiệp chuyên ngành CNTT hoặc lĩnh vực liên quan.</li>
            <li>Có ít nhất 1 năm kinh nghiệm phát triển React, .NET.</li>
            <li>Từng làm việc với RESTful APIs, Git, quy trình Agile/Scrum.</li>
            <li>Ưu tiên: biết Python, Docker, triển khai CI/CD.</li>
          </ul>
        </div>

        <!-- Thông tin bổ sung -->
        <div>
          <h3 class="mb-2 text-xl font-semibold">Benefits & Perks</h3>
          <ul class="list-inside list-disc space-y-1 text-gray-600">
            <li>Lương thưởng hấp dẫn, xét tăng lương định kỳ.</li>
            <li>Chế độ bảo hiểm đầy đủ theo quy định, bảo hiểm sức khoẻ mở rộng.</li>
            <li>Teambuilding, du lịch hàng năm, môi trường trẻ trung.</li>
            <li>Cơ hội làm việc với các chuyên gia hàng đầu, thăng tiến lên vị trí cao.</li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</div>

</template>