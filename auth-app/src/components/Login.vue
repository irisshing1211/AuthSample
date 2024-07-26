<template>
  <div>
    <h2>Login</h2>
    <form @submit.prevent="login">
      <input v-model="username" placeholder="Username" />
      <input v-model="password" type="password" placeholder="Password" />
      <button type="submit">Login</button>
    </form>
    <div class="register-link">
      <p>Don't have an account?</p>
      <router-link to="/register">Register here</router-link>
    </div>
  </div>
</template>

<script>
export default {
  name:"LoginForm",
  data() {
    return {
      username: '',
      password: ''
    };
  },
  methods: {
    async login() {
      try {
        const response = await fetch('https://localhost:5001/api/auth/login', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({ username: this.username, password: this.password })
        });

        if (response.ok) {
          // Handle success
        } else {
          // Handle error
        }
      } catch (error) {
        console.error('Error:', error);
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
