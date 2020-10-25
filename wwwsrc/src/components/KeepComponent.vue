<template>
  <div class="col-6 col-md-3 card">
      <img :src="keepProp.img" alt="">
      <h2>{{keepProp.name}}</h2>
      <img @click="viewProfile" :src="keepProp.creator.picture" alt="">
  </div>
</template>

<script>
export default {
    name: 'keep-component',
    props: ["keepProp"],
    methods:{
        viewProfile(){
            this.$router.push({name: "Profile", params: {id: this.keepProp.creator.id}})
        }
    },

}
</script>

<style>

</style>