<template>
  <v-app>
  <v-app-bar app>
    <h3>Derivitec Timer Demo</h3>
  </v-app-bar>
  
  <!-- Sizes your content based upon application components -->
  <v-main>
      <!-- Provides the application the proper gutter -->
      <v-container fluid>
        <router-view></router-view>
      </v-container>
  </v-main>
  <v-footer app>
    <!-- -->
  </v-footer>
</v-app>

</template>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  height:100vh;
}
#nav {
  padding: 30px;

  a {
    font-weight: bold;
    color: #2c3e50;

    &.router-link-exact-active {
      color: #42b983;
    }
  }
}
</style>
