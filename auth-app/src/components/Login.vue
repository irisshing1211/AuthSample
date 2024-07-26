<template>
  <div>
    <h2>Login</h2>
    <form @submit.prevent="login">
      <input v-model="form.username" placeholder="Username" />
      <input v-model="form.password" type="password" placeholder="Password" />
      <button type="submit">Login</button>
    </form>
    <div class="register-link">
      <p>Don't have an account?</p>
      <router-link to="/register">Register here</router-link>
    </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name:"LoginForm",
  data() {
    return {
    form:{ username: '',
      password: ''} 
    };
  },
  methods: {
    async login() {
      try {
        this.error = null; // 清除任何先前的錯誤
        console.log(this.form)
        const response = await axios.post('https://localhost:5001/api/auth/login', this.form);
        const token = response.data.token;

        // 儲存 token
        localStorage.setItem('authToken', token);

        // 進行後續操作，如導航到主頁
        this.$router.push('/home');
      } catch (error) {
        this.error = 'Login failed. Please try again.';
      }
    }
  }
};
</script>
<style scoped>
.register-link {
  margin-top: 1em;
}
.register-link a {
  color: blue;
  text-decoration: underline;
}
</style>
