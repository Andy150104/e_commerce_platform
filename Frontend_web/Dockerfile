# Stage 1: Build Stage
FROM node:18-alpine AS build-stage

# Set working directory
WORKDIR /app

# Install dependencies
COPY package*.json ./
RUN npm install

# Copy project files
COPY . .

# Build the application
RUN npm run build

# Stage 2: Production Stage
FROM node:18-alpine AS production-stage

# Set working directory
WORKDIR /app

# Install dependencies for runtime
COPY package*.json ./
RUN npm install --only=production

# Copy the build files from the build stage
COPY --from=build-stage /app/.output ./.output

# Expose the port Nuxt.js uses
EXPOSE 3000
ENV HOST=0.0.0.0 PORT=3000 NODE_ENV=production
# Run the Nuxt.js application
CMD ["npm", "run", "start"]
