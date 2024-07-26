<template>
  <div class="register">
    <h2>Register</h2>
    <form @submit.prevent="handleSubmit">
      <div>
        <label for="username">Username:</label>
        <input v-model="form.username" type="text" id="username" required />
      </div>
      <div>
        <label for="password">Password:</label>
        <input v-model="form.password" type="password" id="password" required />
      </div>
      <div>
        <label for="name">Name:</label>
        <input v-model="form.name" type="text" id="name" required />
      </div>
      <div>
        <label for="name">Email:</label>
        <input v-model="form.email" type="email" id="email" required />
      </div>
      <button type="submit">Register</button>
    </form>
    <div v-if="error" class="error">{{ error }}</div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name:"RegisterForm",
  data() {
    return {
      form: {
        username: '',
        password: '',
        email:'',
        name: '',
      },
      error: null,
    };
  },
  methods: {
    async handleSubmit() {
      try {
        this.error = null; // 清除錯誤信息
        await axios.post('https://localhost:5001/api/auth/register', this.form);
        // 處理成功邏輯，例如重定向到登入頁
        this.$router.push('/login');
      } catch (error) {
        // 顯示錯誤信息
        this.error = 'Registration failed. Please try again.';
      }
    },
  },
};
</script>

<style scoped>
.error {
  color: red;
}
</style>
