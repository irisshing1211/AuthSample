<template>
  <div>
    <div v-for="(item, index) in localData" :key="index" :style="{ paddingLeft: `${level * 20}px` }">
      <span>{{ item.name }}</span>
      <!-- 遞歸顯示子項目 -->
      <MenuList
          :data="item.children"
          :level="level + 1"
          v-if="item.children && item.children.length"
      />
    </div>
  </div>
</template>

<script>
export default {
  name: 'MenuList',
  props: {
    data: {
      type: Array,
      required: true
    },
    level: {
      type: Number,
      default: 0
    }
  },
  data() {
    return {
      localData: [...this.data]
    };
  },
  watch: {
    data: {
      handler(newData) {
        this.localData = [...newData];
      },
      deep: true
    }
  }
}
</script>

<style scoped>
/* 你可以在這裡添加其他樣式 */
</style>
